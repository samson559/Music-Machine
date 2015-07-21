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
		//Vector3 where = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.x, 0);
		Object go = Instantiate(note,Vector3.zero,Quaternion.identity);
	}

}
