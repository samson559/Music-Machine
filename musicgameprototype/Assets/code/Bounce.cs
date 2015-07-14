using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

	public float force = 300;

	/*
	 * Can't this be done through the physics material for the ball/marble? It bounces when I set the bounciness to 1.
	 * - Nathaniel
	 */
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        col.collider.attachedRigidbody.AddForce(Vector2.up * force);
    }
}
