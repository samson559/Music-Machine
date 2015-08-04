using UnityEngine;
using System.Collections;

public class HelpPanelSpawner : MonoBehaviour {
	[SerializeField] private GameObject panel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void showPanel()
	{
		Debug.Log ("try");
		Instantiate<GameObject> (panel);
	}
}
