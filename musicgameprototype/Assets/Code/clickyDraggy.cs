//Dylan Noaker's code
//cannot enable/disable this script. 
using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Collider2D))]
public class clickyDraggy : MonoBehaviour {
	[SerializeField] private float mouseTolerance = .1f;
	private Transform lastTransform = null;
	private Ray ray;
	private RaycastHit hit;
	private Vector3 screenPoint;
	private Vector3 offset;
	private float prevMouse;
	//This is from http://answers.unity3d.com/questions/12322/drag-gameobject-with-mouse.html
	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
	}
	
	void OnMouseDrag()
	{

		if (Input.GetButton ("rotateOption")) {
			transform.Rotate (new Vector3 (0, 0, Input.mousePosition.y - prevMouse));
			prevMouse = Input.mousePosition.y;
			return;
		}
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+ offset;
		transform.position = curPosition;
		prevMouse = Input.mousePosition.y;
	}
	// end citation
	// Use this for initialization
	void Start () {
	
	}

	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetMouseButton(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray,out hit))
			{
				float newx = transform.position.x + Input.GetAxis("Mouse X");
				float newy = transform.position.y + Input.GetAxis("Mouse Y");
				//Debug.Log (hit.point);
				//Debug.DrawRay(ray.origin,hit.point);
				transform.position = new Vector3(newx,newy,transform.position.z);
			}
		}
		*/
	}


}
