using UnityEngine;
using System.Collections.Generic;

// TODO: No hardcoding
public class Calibration : MonoBehaviour {
	
	private Dictionary<KeyCode, SpriteRenderer> spritesByKey;
	private int numSensorsTriggered = 0;

	// Use this for initialization
	void Start () {
		spritesByKey = new Dictionary<KeyCode, SpriteRenderer>();
		spritesByKey[KeyCode.U] = GameObject.Find("Head Red").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.A] = GameObject.Find("Left Arm Red").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.G] = GameObject.Find("Right Arm Red").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.J] = GameObject.Find("Left Leg Red").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.D] = GameObject.Find("Right Leg Red").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.Q] = GameObject.Find("Head Blue").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.W] = GameObject.Find("Left Arm Blue").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.E] = GameObject.Find("Right Arm Blue").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.R] = GameObject.Find("Left Leg Blue").GetComponent<SpriteRenderer>();
		spritesByKey[KeyCode.T] = GameObject.Find("Right Leg Blue").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(KeyValuePair<KeyCode, SpriteRenderer> keySprite in spritesByKey) {
			if (Input.GetKeyDown(keySprite.Key) && keySprite.Value.color.a < 1.0f) {
				keySprite.Value.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				numSensorsTriggered++;
			}
		}
		if (numSensorsTriggered == 10 || Input.GetMouseButtonDown (0)) {
			Application.LoadLevel("MainScene");
		}
	}
}
