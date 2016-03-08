using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIViewController : MonoBehaviour {

	public GameLogic gameLogic;
	public Text countdownText;
	public Text roundText;
	public GameObject freezeFrameFlash;
	public GameObject tutorial;
	public GameObject endText;
	public GameObject startingLineText;

	// Left side
	public GameObject redNinja;
	public Text scoreA;
	public GameObject targetOnPlayerB;
	// Indexed by body part enum (TODO: come up with a better solution)
	public GameObject[] partSpritesPlayerA;
	public GameObject[] checkMarksPlayerA;
	public GameObject[] xMarksPlayerA;

	// Right side
	public GameObject blueNinja;
	public Text scoreB;
	public GameObject targetOnPlayerA;
	// Indexed by body part enum (TODO: come up with a better solution)
	public GameObject[] partSpritesPlayerB;
	public GameObject[] checkMarksPlayerB;
	public GameObject[] xMarksPlayerB;

	// Use this for initialization
	void Start () {
		gameLogic.PreGameStartEvent += new EventHandler(this.OnPreGameStartEvent);
		gameLogic.PreGameEndEvent += new EventHandler(this.OnPreGameEndEvent);
		
		gameLogic.CountdownStartEvent += new EventHandler(this.OnCountdownStartEvent);
		gameLogic.CountdownNewSecondEvent += new EventHandler(this.OnCountdownNewSecondEvent);
		gameLogic.CountdownEndEvent += new EventHandler(this.OnCountdownEndEvent);
		
		gameLogic.PlayStartEvent += new EventHandler(this.OnPlayStartEvent);
		gameLogic.WrongHitEvent += new EventHandler(this.OnWrongHitEvent);
		gameLogic.PlayEndEvent += new EventHandler(this.OnPlayEndEvent);

		gameLogic.FreezeFrameStartEvent += new EventHandler(this.OnFreezeFrameStartEvent);
		gameLogic.FreezeFrameEndEvent += new EventHandler(this.OnFreezeFrameEndEvent);
		
		gameLogic.PostGameStartEvent += new EventHandler(this.OnPostGameStartEvent);
		gameLogic.PostGameEndEvent += new EventHandler(this.OnPostGameEndEvent);

	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnPreGameStartEvent(object sender, EventArgs e) {
		tutorial.SetActive(true);
	}
	
	private void OnPreGameEndEvent(object sender, EventArgs e) {
		tutorial.SetActive(false);
	}
	
	private void OnCountdownStartEvent(object sender, EventArgs e) {
		roundText.text = "Round " + gameLogic.currentRound + "/10";
		startingLineText.SetActive(true);
	}
	
	private void OnCountdownNewSecondEvent(object sender, EventArgs e) {
		if (gameLogic.countDownSecondsLeft != 0) {
			print ("COUNTDOWN: " + gameLogic.countDownSecondsLeft);
			countdownText.text = "" + gameLogic.countDownSecondsLeft;
		}
	}

	private void OnCountdownEndEvent(object sender, EventArgs e) {
		startingLineText.SetActive(false);
	}
	
	private void OnPlayStartEvent(object sender, EventArgs e) {
		targetOnPlayerA.SetActive(true);
		targetOnPlayerB.SetActive(true);
		targetOnPlayerA.GetComponent<Target>().Reposition(gameLogic.targetOnPlayerA);
		targetOnPlayerB.GetComponent<Target>().Reposition(gameLogic.targetOnPlayerB);
		countdownText.text = "TOUCH!";
		countdownText.gameObject.GetComponent<Animator>().SetTrigger("StartPlay");
	}
	
	private void OnWrongHitEvent(object sender, EventArgs e) {
		if (gameLogic.lastHit == Player.A) {
			print ("Wrong Hit: " + gameLogic.lastHitOnPlayerA);
			xMarksPlayerA[(int)gameLogic.lastHitOnPlayerA].GetComponent<Animator>().SetTrigger("fadeInX");
		} else if (gameLogic.lastHit == Player.B) {
			print ("Wrong Hit: " + gameLogic.lastHitOnPlayerB);
			xMarksPlayerB[(int)gameLogic.lastHitOnPlayerB].GetComponent<Animator>().SetTrigger("fadeInX");
		}
	}

	private void OnPlayEndEvent(object sender, EventArgs e) {
		if (gameLogic.lastHit == Player.A && gameLogic.targetOnPlayerA == gameLogic.lastHitOnPlayerA) {
			checkMarksPlayerA[(int)gameLogic.lastHitOnPlayerA].GetComponent<Animator>().SetTrigger("fadeInCheck");
			partSpritesPlayerA[(int)gameLogic.lastHitOnPlayerA].GetComponent<Animator>().SetTrigger("hit");
			countdownText.text = "RED +1";
		} else if (gameLogic.lastHit == Player.B && gameLogic.targetOnPlayerB == gameLogic.lastHitOnPlayerB) {
			checkMarksPlayerB[(int)gameLogic.lastHitOnPlayerB].GetComponent<Animator>().SetTrigger("fadeInCheck");
			partSpritesPlayerB[(int)gameLogic.lastHitOnPlayerB].GetComponent<Animator>().SetTrigger("hit");
			countdownText.text = "BLUE +1";
		} else {
			countdownText.text = "NO STRIKE";
		}
		countdownText.gameObject.GetComponent<Animator>().SetTrigger("EndPlay");
		targetOnPlayerA.SetActive(false);
		targetOnPlayerB.SetActive(false);
		scoreA.text = "" + gameLogic.scoreA;
		scoreB.text = "" + gameLogic.scoreB;
	}

	private void OnFreezeFrameStartEvent(object sender, EventArgs e) {
		if (gameLogic.lastHit == Player.A && gameLogic.targetOnPlayerA == gameLogic.lastHitOnPlayerA ||
		    gameLogic.lastHit == Player.B && gameLogic.targetOnPlayerB == gameLogic.lastHitOnPlayerB) {
			freezeFrameFlash.GetComponent<Animator>().SetTrigger("flash");
		}
	}
	
	private void OnFreezeFrameEndEvent(object sender, EventArgs e) {}
	
	private void OnPostGameStartEvent(object sender, EventArgs e) {
		endText.SetActive(true);
		if (gameLogic.scoreA > gameLogic.scoreB) {
			countdownText.text = "BLUE WINS!";
			partSpritesPlayerA[(int)BodyPart.rightArm].GetComponent<Animator>().SetTrigger("victory");
			redNinja.GetComponent<Animator>().SetTrigger("loss");
			StartCoroutine(HideRedNinjaAfterLoss());
		} else if (gameLogic.scoreB > gameLogic.scoreA) {
			countdownText.text = "RED WINS!";
			partSpritesPlayerB[(int)BodyPart.leftArm].GetComponent<Animator>().SetTrigger("victory");
			blueNinja.GetComponent<Animator>().SetTrigger("loss");
			StartCoroutine(HideBlueNinjaAfterLoss());
		} else {
			countdownText.text = "DRAW!";
			StartCoroutine(HideBlueNinjaAfterLoss());
			StartCoroutine(HideRedNinjaAfterLoss());
		}
	}
	
	private void OnPostGameEndEvent(object sender, EventArgs e) {
		endText.SetActive(false);
	}

	// TODO: Combine these two
	IEnumerator HideRedNinjaAfterLoss() {
		yield return new WaitForSeconds(2.0f);
		redNinja.SetActive(false);
	}

	IEnumerator HideBlueNinjaAfterLoss() {
		yield return new WaitForSeconds(2.0f);
		blueNinja.SetActive(false);
	}
}
