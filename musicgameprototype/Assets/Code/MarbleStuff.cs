//Dylan Noaker's code
using UnityEngine;
using System.Collections;

public class MarbleStuff : MonoBehaviour {
	private GameObject ball;
	// Use this for initialization
	void Start () {
		ball = transform.FindChild ("Ball").gameObject;
		ball.GetComponent<Rigidbody2D> ().Sleep ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Restart()
	{
		//put ball back at origin
		ball.transform.position = this.transform.position;
		ball.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		ball.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
	}
	// grabbed from http://answers.unity3d.com/questions/171492/how-to-make-a-pause-menu-in-c.html
	public void togglePause(string thing)
	{
		Debug.Log ("Hello");
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
		}
		else
		{
			Time.timeScale = 0f;    
		}
	}
	//end grab.
}
