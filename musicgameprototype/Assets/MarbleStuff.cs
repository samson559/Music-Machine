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
}
