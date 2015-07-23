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
	public void OnMouseDown()
	{
		// delete when hold 'deleteOption' key
		if (Input.GetButton ("deleteOption")) {
			Destroy (gameObject);
			return;
		}

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

	}


}
