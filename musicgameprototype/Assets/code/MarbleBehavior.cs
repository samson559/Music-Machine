using UnityEngine;
using System.Collections;

/*
 * This class handles marble/ball behavior, such as destroying the ball after it falls past a certain point.
 * It is attached to the Marble prefab.
 */

public class MarbleBehavior : MonoBehaviour {

	private Vector3 origin;
	private Transform t;
	private float minX, minY, maxX, maxY; // marble boundaries based on stage dimensions
	private StageData stageData;
	private Vector2 savedVelocity; // used when pausing and restarting physics.


	// Use this for initialization
	void Start () {
		GameObject camera = GameObject.Find ("Camera");

		stageData = Camera.main.GetComponent<StageData> () as StageData;

		minX = -stageData.getStageWidth () / 2;
		maxX = minX + stageData.getStageWidth ();
		minY = -stageData.getStageHeight () / 2;
		maxY = minY + stageData.getStageHeight ();

		t = GetComponent<Transform>();
		origin = new Vector3 (t.position.x, t.position.y, t.position.z);

	}
	
	// Update is called once per frame
	void Update () {
		// destroy when marble is offscreen
		//if ((t.position.x < minX) || (t.position.x > maxX) || (t.position.y < minY) || (t.position.y > maxY))
			//Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		//Debug.Log (col.gameObject.name);
	}

	public void setSavedVelocity(Vector2 vel) {
		savedVelocity = vel;
	}
	
	public Vector2 getSavedVelocity() {
		return savedVelocity;
	}
}
