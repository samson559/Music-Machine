using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class musicObjMaker : MonoBehaviour {
	[SerializeField] private GameObject note;

	private Button button;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

//	public void OnPointerDown(PointerEventData data) {
//		makeNote ();
//	}

	public void makeNote(string noteName) {
//		Transform noteT = note.GetComponent<Transform> ();
//
//		// create plane on which to place note
//		Plane gamePlane = new Plane ();
//		Vector3 planePoint1 = new Vector3 (0, 0, noteT.position.z); // noteT already has the correct Z coord, we need to find the right x and y coords...
//		Vector3 planePoint2 = new Vector3 (0, 1, noteT.position.z);
//		Vector3 planePoint3 = new Vector3 (1, 1, noteT.position.z);
//		gamePlane.Set3Points (planePoint1, planePoint2, planePoint3);
//
//		// calculate intersection of gamePlane with a ray going from the camera to the mouse position
//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		float dist; // dist must first be declared
//		Vector3 where = Vector3.zero; // initialize where to (0,0,0)
//		if (gamePlane.Raycast (ray, out dist)) // I am not familiar with C# syntax, but this works...
//			where = ray.GetPoint (dist);
//
//		// instantiate object
//		Object go = Instantiate(note, where, Quaternion.identity);
//		Nate, we're going to try this for awhile

		Vector3 where = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 56.13f);
		GameObject newNote = Instantiate(note, where, Quaternion.identity) as GameObject;
		newNote.GetComponent<MusicObj> ().setNoteName (noteName);
	}

}
