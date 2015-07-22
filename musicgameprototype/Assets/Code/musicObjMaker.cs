using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class musicObjMaker : MonoBehaviour, IPointerDownHandler {
	[SerializeField] private GameObject note;

	private Button button;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnPointerDown(PointerEventData data) {
		makeNote ();
	}

	public void makeNote()
	{
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		Transform noteT = note.GetComponent<Transform> ();
		//Ray ray = Camera.main.ScreenToWorldPoint (new Vector3(mouseX, mouseY, 0));
		Vector3 where = Camera.main.ScreenToWorldPoint (new Vector3 (mouseX, mouseY, noteT.position.z / 2));
		Object go = Instantiate(note, where, Quaternion.identity);
	}

}
