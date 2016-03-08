using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour {

	public GameLogic gameLogic;

	private AudioSource buzzer;
	private AudioSource cameraSound;
	private AudioSource cheering;
	private AudioSource countHigh;
	private AudioSource countLow;
	private AudioSource themeMusic;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		foreach(AudioSource source in audioSources) {
			switch (source.priority) {
			case 1:
				buzzer = source;
				break;
			case 2:
				cameraSound = source;
				break;
			case 3:
				cheering = source;
				break;
			case 4:
				countHigh = source;
				break;
			case 5:
				countLow = source;
				break;
			case 6:
				themeMusic = source;
				break;
			}
		}

		//gameLogic.PreGameStartEvent += new EventHandler(this.OnPreGameStartEvent);

		//gameLogic.CountdownStartEvent += new EventHandler(this.OnCountdownStartEvent);
		gameLogic.CountdownNewSecondEvent += new EventHandler(this.OnCountdownNewSecondEvent);

		//gameLogic.PlayStartEvent += new EventHandler(this.OnPlayStartEvent);
		gameLogic.WrongHitEvent += new EventHandler(this.OnWrongHitEvent);

		//gameLogic.PlayEndEvent += new EventHandler(this.OnPlayEndEvent);
		
		gameLogic.FreezeFrameStartEvent += new EventHandler(this.OnFreezeFrameStartEvent);
		//gameLogic.FreezeFrameEndEvent += new EventHandler(this.OnFreezeFrameEndEvent);
		
		gameLogic.PostGameStartEvent += new EventHandler(this.OnPostGameStartEvent);
		//gameLogic.PostGameEndEvent += new EventHandler(this.OnPostGameEndEvent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCountdownNewSecondEvent(object sender, EventArgs e) {
		if (gameLogic.countdownElapsed >= gameLogic.countdownDuration) {
			countHigh.Play();
		} else {
			countLow.Play();
		}
	}

	private void OnWrongHitEvent(object sender, EventArgs e) {
		buzzer.Play();
	}

	private void OnFreezeFrameStartEvent(object sender, EventArgs e) {
		cameraSound.Play();
	}

	private void OnPostGameStartEvent(object sender, EventArgs e) {
		cheering.Play();
	}
}
