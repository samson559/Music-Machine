using UnityEngine;
using System.Collections;

/*
 * This class handles marble/ball behavior, such as destroying the ball after it falls past a certain point.
 * It is attached to the Marble prefab.
 */

public class MarbleBehavior : MonoBehaviour {
	private Transform t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (t.position.y <= -30) { // Destroy marble after 
			Destroy(gameObject);
		}
	}
}
