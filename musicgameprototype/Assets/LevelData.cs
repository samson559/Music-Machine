using UnityEngine;
using System.Collections;

[System.Serializable]
public class songData {
	public enum Notes {
		A,
		C,
		D
	}
	public Notes songNotes = Notes.A;
	public float noteTime = 0;
	public float bufferTime = 0;
}



public class LevelData : MonoBehaviour {

	public songData[] song;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Material>().color = new Color (0.5f, 0.1f, 1f, 1f);

	}
	
	// Update is called once per frame
	void Update () {
	

	}

}
