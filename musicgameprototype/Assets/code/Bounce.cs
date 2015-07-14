using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

    public float force = 300;
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        col.collider.attachedRigidbody.AddForce(Vector2.up * force);
    }
}
