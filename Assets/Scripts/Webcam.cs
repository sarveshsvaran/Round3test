using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Webcam : MonoBehaviour
{
	public GameLogic gameLogic;
	private int index = 0;
	public MeshRenderer[] UseWebcamTexture;
	private WebCamTexture webcamTexture;
	public Color32[] data;
	public Texture2D screenShot;
	
	void Start()
	{
		gameLogic.FreezeFrameStartEvent += new EventHandler(this.OnFreezeFrameStartEvent);
		gameLogic.FreezeFrameEndEvent += new EventHandler(this.OnFreezeFrameEndEvent);

		webcamTexture = new WebCamTexture();
		foreach(MeshRenderer r in UseWebcamTexture)
		{
			r.material.mainTexture = webcamTexture;
			//screenShot = r.material.mainTexture as Texture2D;
			//screenShot.Apply();
		}
		GetComponent<Renderer>().material.mainTexture = webcamTexture;
		//screenShot = GetComponent<Renderer>().material.mainTexture as Texture2D;
		//screenShot.Apply();
		webcamTexture.Play();
//		screenShot = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
//		Debug.Log ("");
	}
	IEnumerator StartDraw(){
		var x = Screen.width;
		var y = Screen.height;
		yield return new WaitForEndOfFrame ();
		screenShot = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
		screenShot.ReadPixels(new Rect(x/4,y/3,1024,1024),0,0,false);
		screenShot.Apply();
	}

	private void OnFreezeFrameStartEvent(object sender, EventArgs e) {
		if (gameLogic.lastHit == Player.A && gameLogic.targetOnPlayerA == gameLogic.lastHitOnPlayerA ||
		    gameLogic.lastHit == Player.B && gameLogic.targetOnPlayerB == gameLogic.lastHitOnPlayerB) {
			//screenShot = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
			//screenShot = new Texture2D(GetComponent<Renderer>().material.mainTexture.width, GetComponent<Renderer>().material.mainTexture.height, TextureFormat.RGBA32, false);
			//byte[] bytes = screenShot.EncodeToPNG();
			webcamTexture.Pause();

			StartCoroutine("StartDraw");


//			screenShot = GetComponent<Renderer>().material.mainTexture as Texture2D;
//			screenShot.Apply();
		//	File.WriteAllBytes(Application.dataPath + "/Screenshots/" + "Screenshot_" + index + ".png", screenShot.EncodeToPNG());
			//index++;

			//data = new Color32[webcamTexture.width * webcamTexture.height];
			//screenShot = new Texture2D(GetComponent<Renderer>().material.mainTexture.width, GetComponent<Renderer>().material.mainTexture.height, TextureFormat.RGBA32, false);
		}
	}

	private void OnFreezeFrameEndEvent(object sender, EventArgs e) {
		webcamTexture.Play();
	}
}