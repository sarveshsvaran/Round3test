using UnityEngine;
using System.Collections.Generic;
using System;

public enum BodyPart {
	head = 0,
	leftArm,
	rightArm,
	leftLeg,
	rightLeg,
	numBodyParts
}

public enum Player {
	A,
	B,
	noPlayer
}

// We have StartXXX, UpdateXXX, and EndXXX methods for each game state.
// GameState is set in start methods.
// End methods are called from Update methods.
// Start methods are called from End methods.
// We also have StartGame and EndGame methods, each called once respectively at the start and end of entire game.
public enum GameState {
	preGame,
	countdown,
	play,
	freezeFrame,
	postGame,
	finished
}

public class GameLogic : MonoBehaviour {

	// Most events are fired at the end of StartXXX and EndXXX methods (in EndXXX, occurs right before StartYYY)
	// WrongHitEvent is fired every time an incorrect body part is hit.
	// CountdownNewSecondEvent is fired every time a second of the countdown elapses.
	public event EventHandler PreGameStartEvent;
	public event EventHandler PreGameEndEvent;
	public event EventHandler CountdownStartEvent;
	public event EventHandler CountdownNewSecondEvent;
	public event EventHandler CountdownEndEvent;
	public event EventHandler PlayStartEvent;
	public event EventHandler WrongHitEvent;
	public event EventHandler PlayEndEvent;
	public event EventHandler FreezeFrameStartEvent;
	public event EventHandler FreezeFrameEndEvent;
	public event EventHandler PostGameStartEvent;
	public event EventHandler PostGameEndEvent;

	public int countdownDuration { get; private set; }
	public float countdownElapsed { get; private set; }
	public int countDownSecondsLeft { get; private set; }

	public float roundDuration { get; private set; }
	public float roundElapsed { get; private set; }

	public float freezeFrameDuration { get; private set; }
	public float freezeFrameElapsed { get; private set; }

	public int numRounds { get; private set; }
	public int currentRound { get; private set; }

	public int scoreA { get; private set; }
	public int scoreB { get; private set; }
	//public Player lastRoundWinner { get; private set; }

	public BodyPart targetOnPlayerA { get; private set; }
	public BodyPart targetOnPlayerB { get; private set; }

	public BodyPart lastHitOnPlayerA { get; private set; }
	public BodyPart lastHitOnPlayerB { get; private set; }

	public Player lastHit { get; private set; }

	private GameState gameState = GameState.preGame;

	private Dictionary<BodyPart, KeyCode> keycodesA = new Dictionary<BodyPart, KeyCode>() {
		{BodyPart.head, 		KeyCode.Q},		
		{BodyPart.leftArm, 		KeyCode.W},
		{BodyPart.rightArm, 	KeyCode.E},
		{BodyPart.leftLeg, 		KeyCode.R},
		{BodyPart.rightLeg, 	KeyCode.T},
	};

	private Dictionary<BodyPart, KeyCode> keycodesB = new Dictionary<BodyPart, KeyCode>() {
		{BodyPart.head,			KeyCode.U},
		{BodyPart.leftArm,		KeyCode.A},
		{BodyPart.rightArm,		KeyCode.G},
		{BodyPart.leftLeg,		KeyCode.D},
		{BodyPart.rightLeg,		KeyCode.J},
	};

	// Use this for initialization
	void Start () {
		countdownDuration = 3;
		countdownElapsed = 0.0f;
		roundDuration = 3.0f;
		roundElapsed = 0.0f;
		freezeFrameDuration = 2.0f;
		freezeFrameElapsed = 0.0f;
		numRounds = 10;
		currentRound = 1;
		scoreA = 0;
		scoreB = 0;
		lastHit = Player.noPlayer;

		StartGameLoop();
	}
	
	// Update is called once per frame
	void Update () {
		switch (gameState) {
			case GameState.preGame:
				UpdatePregame();
				break;
			case GameState.countdown:
				UpdateCountdown();
				break;
			case GameState.play:
				UpdatePlay();
				break;
			case GameState.freezeFrame:
				UpdateFreezeFrame();
				break;
			case GameState.postGame:
				UpdatePostGame();
				break;
			case GameState.finished:
				break;
		}
	}

	void StartPregame() {
		gameState = GameState.preGame;

		if (this.PreGameStartEvent != null) {
			this.PreGameStartEvent(this, EventArgs.Empty);
		}
	}

	void UpdatePregame() {
		if (Input.GetMouseButtonDown(0)) {
			EndPregame();
		}
	}

	void EndPregame() {
		if (this.PreGameEndEvent != null) {
			this.PreGameEndEvent(this, EventArgs.Empty);
		}
		StartCountdown();
	}

	void StartCountdown() {
		gameState = GameState.countdown;
		countdownElapsed = 0.0f;
		countDownSecondsLeft = (int)countdownDuration;

		if (this.CountdownStartEvent != null) {
			this.CountdownStartEvent(this, EventArgs.Empty);
		}
		if (this.CountdownNewSecondEvent != null) {
			this.CountdownNewSecondEvent(this, EventArgs.Empty);
		}
	}

	void UpdateCountdown() {
		float previousCountdownElapsed = countdownElapsed;
		countdownElapsed += Time.deltaTime;

		if ((int)previousCountdownElapsed != (int)countdownElapsed) {
			countDownSecondsLeft = 3 - (int)countdownElapsed;
			if (this.CountdownNewSecondEvent != null) {
				this.CountdownNewSecondEvent(this, EventArgs.Empty);
			}
		}

		if (countdownElapsed >= countdownDuration) {
			EndCountdown();
		}
	}

	void EndCountdown() {
		if (this.CountdownEndEvent != null) {
			this.CountdownEndEvent(this, EventArgs.Empty);
		}
		StartPlay();
	}

	void StartPlay() {
		gameState = GameState.play;
		roundElapsed = 0.0f;
		targetOnPlayerA = RandomBodyPart();
		targetOnPlayerB = RandomBodyPart();
		lastHit = Player.noPlayer;

		// print ("Starting round: " + currentRound + "\t\tPlayer A Target: " + targetOnPlayerA + "\t\tPlayer B Target: " + targetOnPlayerB);
		// TEMP
		print ("Player B Target: " + targetOnPlayerA + "\t\t\t\t\t\t" + "ROUND " + currentRound + "\t\t\t\t\t\tPlayer A Target: " + targetOnPlayerB);

		if (this.PlayStartEvent != null) {
			this.PlayStartEvent(this, EventArgs.Empty);
		}
	}

	void UpdatePlay() {
		roundElapsed += Time.deltaTime;
		if (roundElapsed >= roundDuration) {
			EndPlay(Player.noPlayer);
			return;
		} else if (Input.GetKeyDown(keycodesA[targetOnPlayerA])) { // Correct hit on Player A
			lastHitOnPlayerA = targetOnPlayerA;
			lastHit = Player.A;
			EndPlay(Player.B);
		} else if (Input.GetKeyDown(keycodesB[targetOnPlayerB])) { // Correct hit on Player B
			lastHitOnPlayerB = targetOnPlayerB;
			lastHit = Player.B;
			EndPlay(Player.A);
		} else { // Incorrect hits... NOTE: Don't return early--allow multiple hits
			foreach (KeyValuePair<BodyPart, KeyCode> partKeycode in keycodesA) {
				if (Input.GetKeyDown(partKeycode.Value)) {
					lastHitOnPlayerA = partKeycode.Key;
					lastHit = Player.A;

					if (this.WrongHitEvent != null) {
						this.WrongHitEvent(this, EventArgs.Empty);
					}
				}
			}
			
			foreach (KeyValuePair<BodyPart, KeyCode> partKeycode in keycodesB) {
				if (Input.GetKeyDown(partKeycode.Value)) {
					lastHitOnPlayerB = partKeycode.Key;
					lastHit = Player.B;

					if (this.WrongHitEvent != null) {
						this.WrongHitEvent(this, EventArgs.Empty);
					}
				}
			}
		}
	}

	void EndPlay(Player roundWinner) {
		print ("Ending round: " + currentRound + "\t\t\t\t\t\tWinner: " + roundWinner);
		switch (roundWinner) {
			case Player.A:
				scoreA++;
				//lastRoundWinner = Player.A;
				break;
			case Player.B:
				scoreB++;
				//lastRoundWinner = Player.B;
				break;
			case Player.noPlayer:
				//lastRoundWinner = Player.noPlayer;
				break;
		}

		if (this.PlayEndEvent != null) {
			this.PlayEndEvent(this, EventArgs.Empty);
		}
		StartFreezeFrame();
	}

	void StartFreezeFrame() {
		gameState = GameState.freezeFrame;

		print ("Freeze frame start");

		freezeFrameElapsed = 0.0f;

		if (this.FreezeFrameStartEvent != null) {
			this.FreezeFrameStartEvent(this, EventArgs.Empty);
		}
	}

	void UpdateFreezeFrame() {
		freezeFrameElapsed += Time.deltaTime;

		if (freezeFrameElapsed >= freezeFrameDuration) {
			EndFreezeFrame();
		}
	}

	void EndFreezeFrame() {
		print ("Freeze frame end");

		currentRound++;

		if (this.FreezeFrameEndEvent != null) {
			this.FreezeFrameEndEvent(this, EventArgs.Empty);
		}
		if (currentRound <= numRounds) {
			StartCountdown();
		} else {
			StartPostGame();
		}
	}

	void StartPostGame() {
		gameState = GameState.postGame;
		if (scoreA > scoreB) {
			print("Player A wins!");
		} else if (scoreB > scoreA) {
			print("Player B wins!");
		} else {
			print("Tie!");
		}

		if (this.PostGameStartEvent != null) {
			this.PostGameStartEvent(this, EventArgs.Empty);
		}
	}
	
	void UpdatePostGame() {
		if (Input.GetMouseButtonDown(0)) {
			EndPostGame();
		}
	}
	
	void EndPostGame() {
		if (this.PostGameEndEvent != null) {
			this.PostGameEndEvent(this, EventArgs.Empty);
		}
		EndGameLoop();
	}
	
	void StartGameLoop() {
		StartPregame();
	}

	void EndGameLoop() {
		gameState = GameState.finished;
		Application.LoadLevel("Calibration");
	}

	BodyPart RandomBodyPart() {
		return (BodyPart)UnityEngine.Random.Range((int)BodyPart.head, (int)BodyPart.numBodyParts);
	}
}
