using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {
	public int resolution = 3; // 1= default, 2= 2x default, etc.
	public string imageName = "Screenshot_";
	public string customPath = ;// = "C:/Users/default/Desktop/UnityScreenshots/"; // leave blank for project file location
	public bool resetIndex = false;
	private int index = 0;
	void Awake()
	{
		if(resetIndex) PlayerPrefs.SetInt("ScreenshotIndex", 0);
		if(customPath != "")
		{
			if(!System.IO.Directory.Exists(customPath))
			{
				System.IO.Directory.CreateDirectory(customPath);
			}
		}
		index = PlayerPrefs.GetInt("ScreenshotIndex") != 0 ? PlayerPrefs.GetInt("ScreenshotIndex") : 1;
	}
//	public int resWidth = 2550; 
//	public int resHeight = 3300;
//	
//	
//	private bool takeHiResShot = false;
//	public static string ScreenShotName(int width, int height) {
//		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
//		                     Application.dataPath, 
//		                     width, height, 
//		                     System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
//	}
//	public void TakeHiResShot() {
//		takeHiResShot = true;
//	}
//	void LateUpdate() {
//		takeHiResShot |= Input.GetKeyDown("k");
//		if (takeHiResShot) {
//			RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
//			Camera.targetTexture = rt;
//			Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
//			Camera.Render();
//			RenderTexture.active = rt;
//			screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
//			Camera.targetTexture = null;
//			RenderTexture.active = null; // JC: added to avoid errors
//			Destroy(rt);
//			byte[] bytes = screenShot.EncodeToPNG();
//			string filename = ScreenShotName(resWidth, resHeight);
//			System.IO.File.WriteAllBytes(filename, bytes);
//			Debug.Log(string.Format("Took screenshot to: {0}", filename));
//			takeHiResShot = false;
//		}
//	}
	void LateUpdate () 
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			Application.CaptureScreenshot(customPath + imageName + index + ".png", resolution);
			index++;
			Debug.LogWarning("Screenshot saved: " + customPath + " --- " + imageName + index);
		}
	}
	
	void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("ScreenshotIndex", (index));
	}
}
