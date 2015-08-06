using UnityEngine;
using System.Collections;

public class CannonRotator : MonoBehaviour {
	[SerializeField] private float rotationRate;
	[SerializeField] private float bob;
	[SerializeField] private float bobRate;
	private bool up= true;
	private float bobTemp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (bobTemp > bob)
			up = false;
		else if (bobTemp <= 0)
			up = true;
		if (up) {
			bobTemp += Time.deltaTime * bob;
			transform.position += Vector3.up * bobRate;
		} else {
			bobTemp -= Time.deltaTime * bob;
			transform.position -= Vector3.up * bobRate;
		}


		transform.Rotate (new Vector3(0,rotationRate,0));
	}
}
