using UnityEngine;
//using UnityEditor;
using System.Collections;

/*
 * This script controls how the marble spawner behaves.
 * Eventually the spawner should only drop on the beat (not off tempo by a few milliseconds), and should be able to drop a ball after every interval as specified by the player.
 * - Nathaniel
 */

public class MarbleSpawnBehavior : MonoBehaviour {
	
	
	
	private bool complete; // once this is true, no more ball spawning!
	
	// Use this for initialization
	void Start () {
		complete = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!complete) {
			Transform marbleT, spawnT; // marble's transform and the spawner's transform
			GameObject marble; // the marble to be instatiated
			//this is weird code... I found it somewhere... I'm not sure what's going on... but it works...
			Object prefab = Resources.Load("Marble.prefab", typeof(GameObject));
			marble = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
			
			marbleT = marble.GetComponent<Transform>();
			spawnT = GetComponent<Transform>();
			
			Vector3 spawnPosition = spawnT.position;
			Vector3 newMarblePosition = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z);
			
			marbleT.position = newMarblePosition;
			
			complete = true;
		}
	}
	
	public void setComplete(bool c) {
		complete = c;
	}
}
