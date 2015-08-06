using UnityEngine;
using System.Collections;

public class CannonRotator : MonoBehaviour {
	[SerializeField] private float rotationRate;
	[SerializeField] private float bob;
	[SerializeField] private float bobRate;
	private bool up= true;
	private float bobTemp;
	private MarbleSpawnBehavior msb;
	private Vector3 bobOffset;
	// Use this for initialization
	void Start () {
		bobOffset = Vector3.zero; // initilize to (0, 0, 0)
	}
	
	// Update is called once per frame
	void Update () {

		//needs a way to make sure the marble spawns from the same place

		if (bobTemp > bob)
			up = false;
		else if (bobTemp <= 0)
			up = true;
		if (up) {
			bobTemp += Time.deltaTime * bob;
			transform.position += Vector3.up * bobRate;
			bobOffset -= Vector3.up * bobRate;
		} else {
			bobTemp -= Time.deltaTime * bob;
			transform.position -= Vector3.up * bobRate;
			bobOffset += Vector3.up * bobRate;
		}


		transform.Rotate (new Vector3(0,rotationRate,0));
	}

	public Vector3 getBobOffset() {
		return bobOffset;
	}
}
