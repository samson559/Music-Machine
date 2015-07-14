using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	/*
	 * This class handles player input, such as pressing the pause button to pause playback.
	 * It is attached to the Camera prefab.
	 */

	// Use this for initialization
	void Start () {
		Time.timeScale = 0; // start the simualtion paused.
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			if(Time.timeScale == 0) Time.timeScale = 1; // restart the simulation;
			else Time.timeScale = 0; // pause the simulation.
		}

	}
}
