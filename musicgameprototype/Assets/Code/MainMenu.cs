////mainmenu
/// Attached to camera
using UnityEngine;
using System.Collections;
public class MainMenu : MonoBehaviour {
	[SerializeField] private Texture Mainmenutexture;
	[SerializeField] private Texture[] LevelThumb;
	[SerializeField] private string[] levelnames;



	void OnGUI(){

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Mainmenutexture);
	
///Button display
		/// Make sure levels are included in build settings and adhere to naming convention "LevelX".
		/// level1
		/*
		if (GUI.Button (new Rect(Screen.width * .5f, Screen.height * .5f, Screen.width * .5f, Screen.height * .1f), new GUIContent("Play Level1",Level1Thumb),"")) {
			Application.LoadLevel(1);
		}
		///level2
		if (GUI.Button (new Rect(Screen.width * .5f, Screen.height * .6f, Screen.width * .5f, Screen.height * .1f), new GUIContent("Play Level2",Level2Thumb),"")) {
			Application.LoadLevel("Level2");
		}
		///level2
		if (GUI.Button (new Rect(Screen.width * .5f, Screen.height * .7f, Screen.width * .5f, Screen.height * .1f), new GUIContent("Play Level3",Level3Thumb),"")) {
			Application.LoadLevel("Level3");
		}
		*/
		int i = 1;
		foreach (Texture T in LevelThumb) {
			if (GUI.Button (new Rect(Screen.width * .5f, Screen.height * (.5f+(i*.1f)), 
			                         Screen.width * .5f, Screen.height * .1f),
			                		new GUIContent("Play Level2",T),""))
			{
				Application.LoadLevel(levelnames[i-1]);
			}
		}

	}
}
