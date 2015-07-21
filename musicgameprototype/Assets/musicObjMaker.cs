using UnityEngine;
using System.Collections;

public class musicObjMaker : MonoBehaviour {
	[SerializeField] private GameObject note;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void makeNote()
	{
		GameObject go = Instantiate<GameObject> (note);
	}

}
