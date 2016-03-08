using UnityEngine;
using System.Collections;
using System;

public class SampleEventSubscriber : MonoBehaviour {

	public GameLogic gameLogic;

	private bool isCountingDown;

	// Use this for initialization
	void Start () {
		gameLogic.PreGameStartEvent += new EventHandler(this.OnPreGameStartEvent);
		gameLogic.PreGameEndEvent += new EventHandler(this.OnPreGameEndEvent);

		gameLogic.CountdownStartEvent += new EventHandler(this.OnCountdownStartEvent);
		gameLogic.CountdownEndEvent += new EventHandler(this.OnCountdownEndEvent);

		gameLogic.PlayStartEvent += new EventHandler(this.OnPlayStartEvent);
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
	}

	private void OnPreGameEndEvent(object sender, EventArgs e) {
	}

	private void OnCountdownStartEvent(object sender, EventArgs e) {
	}

	private void OnCountdownEndEvent(object sender, EventArgs e) {
	}

	private void OnPlayStartEvent(object sender, EventArgs e) {
	}

	private void OnPlayEndEvent(object sender, EventArgs e) {
	}

	private void OnFreezeFrameStartEvent(object sender, EventArgs e) {
	}

	private void OnFreezeFrameEndEvent(object sender, EventArgs e) {
	}

	private void OnPostGameStartEvent(object sender, EventArgs e) {
	}

	private void OnPostGameEndEvent(object sender, EventArgs e) {
	}
	
}
