using UnityEngine;
using System.Collections;

public class clickyDraggy : MonoBehaviour {
	[SerializeField] private float mouseTolerance = .1f;
	private Transform lastTransform = null;
	private Ray ray;
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray,out hit))
			{
				transform.position = new Vector3(hit.point.x,hit.point.y,transform.position.z);
			}
		}
	}

}
