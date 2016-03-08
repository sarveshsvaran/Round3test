using UnityEngine;
using System.Collections;
using System;

public class CountdownAudio : MonoBehaviour {

	public GameLogic gameLogic;

	private AudioSource countdownNoise;

	// Use this for initialization
	void Start () {
		gameLogic.CountdownStartEvent += new EventHandler(this.OnCountdownStartEvent);
		countdownNoise = GetComponent<AudioSource>();
	}

	private void OnCountdownStartEvent(object sender, EventArgs e) {
		countdownNoise.Play();
	}
}
