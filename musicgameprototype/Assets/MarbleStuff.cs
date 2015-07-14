//Dylan Noaker's code
using UnityEngine;
using System.Collections;

public class MarbleStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Restart()
	{
		//put ball back at origin
		transform.FindChild ("Ball").position = Vector3.zero;
	}
	// grabbed from http://answers.unity3d.com/questions/171492/how-to-make-a-pause-menu-in-c.html
	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
	//end grab.
}
