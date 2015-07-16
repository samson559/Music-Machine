using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	public float dragSpeed = -0.5f;
	private Vector3 dragOrigin;
	
	/*
	 * This class handles player input, such as pressing the pause button to pause playback.
	 * It is attached to the Camera prefab.
	 */
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetButton ("panOption")) mousePan ();
		
		if (Input.GetButtonDown ("Pause")) {
			if(Time.timeScale == 0) Time.timeScale = 1; // restart the simulation;
			else Time.timeScale = 0; // pause the simulation.
		}
		
	}
	
	void mousePan() {
		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
			return;
		}
		
		if (Input.GetButton ("reset")) {
			MarbleSpawnBehavior[] msb = FindObjectsOfType<MarbleSpawnBehavior>() as MarbleSpawnBehavior[];
			for(int i = 0; i < msb.Length; i++) {
				msb[i].setComplete(false);
			}
			
			//Destroy(GameObject.FindObjectsOfType<MarbleBehavior>());
			
			/*
			MarbleSpawnBehavior[] mb = FindObjectsOfType<MarbleBehavior>() as MarbleBehavior[];
			for(int i = 0; i < mb.Length; i++) {
			}*/
		}
		
		if (!Input.GetMouseButton(0)) return;
		
		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
		
		transform.Translate(move, Space.World);  
	}
}