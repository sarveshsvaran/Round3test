using UnityEngine;
using System.Collections;
using System;

public class PartAudio : MonoBehaviour {

	public GameLogic gameLogic;

	private AudioSource correctHit;
	private AudioSource wrongHit;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		foreach(AudioSource source in audioSources) {
			if (source.priority == 1) {
				correctHit = source;
			} else if (source.priority == 2) {
				wrongHit = source;
			}
		}

		gameLogic.WrongHitEvent += new EventHandler(this.OnWrongHitEvent);
		gameLogic.PlayEndEvent += new EventHandler(this.OnPlayEndEvent);
	}

	private void OnWrongHitEvent(object sender, EventArgs e) {
		wrongHit.Play();
	}

	private void OnPlayEndEvent(object sender, EventArgs e) {
		if ((gameLogic.lastHit == Player.A && gameLogic.targetOnPlayerA == gameLogic.lastHitOnPlayerA) ||
		    (gameLogic.lastHit == Player.B && gameLogic.targetOnPlayerB == gameLogic.lastHitOnPlayerB)) {
			correctHit.Play ();
		}
	}
}
