using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public float dragSpeed = 0.5f;
	private Vector3 dragOrigin;
	private float minX, minY, maxX, maxY; // min and max X and Y position for camera, based on size of stage

	/*
	 * This class handles player input, such as pressing the pause button to pause playback, pressing the reset button to reset the level, etc.
	 * It is attached to the Camera prefab.
	 */

	// Use this for initialization
	void Start () {
		float stageWidth = (gameObject.GetComponent<StageData> () as StageData).getStageWidth();
		float stageHeight = (gameObject.GetComponent<StageData> () as StageData).getStageHeight();
		
		minX = (gameObject.GetComponent<Transform> () as Transform).position.x - (stageWidth / 2);
		minY = (gameObject.GetComponent<Transform> () as Transform).position.y - (stageHeight / 2);
		maxX = minX + stageWidth;
		maxY = minY + stageHeight;

		Time.timeScale = 0; // start the simualtion paused.
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButton ("panOption")) mousePan ();
		
		if (Input.GetButton ("reset")) {
			MarbleSpawnBehavior[] msb = FindObjectsOfType<MarbleSpawnBehavior>() as MarbleSpawnBehavior[];
			for(int i = 0; i < msb.Length; i++) {
				msb[i].setComplete(false);
			}
		}

		if (Input.GetButtonDown ("Pause")) {
			if(Time.timeScale == 0) Time.timeScale = 1; // restart the simulation;
			else Time.timeScale = 0; // pause the simulation.
		}

	}

	void mousePan() {
		if (Input.GetMouseButtonDown(0)) {
			dragOrigin = Input.mousePosition;
			return;
		}
		
		if (!Input.GetMouseButton(0)) return;
		
		Vector3 cameraPos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 cameraMove = new Vector3(cameraPos.x * dragSpeed, cameraPos.y * dragSpeed, 0);

		Transform cameraT = gameObject.GetComponent<Transform> () as Transform;
		
		cameraT.Translate (cameraMove);

		// constrain camera to stage size
		float newX = cameraT.position.x;
		float newY = cameraT.position.y;
		if (cameraT.position.x < minX)
			newX = minX;
		else if (cameraT.position.x > maxX)
			newX = maxX;
		if (cameraT.position.y < minY)
			newY = minY;
		else if (cameraT.position.y > maxY)
			newY = maxY;

		cameraT.position = new Vector3 (newX, newY, cameraT.position.z);


		Debug.Log (cameraT.position);  
	}
}
