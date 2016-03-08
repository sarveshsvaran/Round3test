using UnityEngine;
using System.Collections;
using System.IO;

public class Read_Pixels : MonoBehaviour {

	// Use this for initialization
	public float timer;
	void Start () {
		gameObject.GetComponent<Renderer> ().enabled = false;
		timer = 300;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown("c"))
		{
			gameObject.GetComponent<Renderer> ().enabled = true;
			Texture2D tex = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
			tex.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0,false);
			tex.Apply();
//			byte[] bytes = tex.EncodeToPNG();
//			File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
			if (GetComponent<Renderer>() != null)
				GetComponent<Renderer>().sharedMaterial.mainTexture = tex;
			StartCoroutine("timerstart");

		}        
	}
	IEnumerator timerstart()
	{
		yield return new WaitForSeconds(2f);
		gameObject.GetComponent<Renderer> ().enabled = false;
	}
}
