using UnityEngine;
using System.Collections;

public class FanDriver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay2D(Collider2D col)
	{
		col.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up*50);
	}
}
