using UnityEngine;
using System.Collections;

/*
 * This class handles marble/ball behavior, such as destroying the ball after it falls past a certain point.
 * It is attached to the Marble prefab.
 */

public class MarbleBehavior : MonoBehaviour {
	private Vector3 origin;
	private Transform t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
		origin = new Vector3 (t.position.x, t.position.y, t.position.z); 
	}
	
	// Update is called once per frame
	void Update () {

	}
}
