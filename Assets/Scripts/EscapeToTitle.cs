using UnityEngine;
using System.Collections;

public class EscapeToTitle : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("Calibration");
		}
	}
}
