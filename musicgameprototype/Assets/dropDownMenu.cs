using UnityEngine;
using System.Collections;

public class dropDownMenu : MonoBehaviour {
	[SerializeField]private GameObject panel;
	// Use this for initialization
	void Start () {
		panel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void toggleMenu()
	{
		if (panel.activeSelf == false) {
			panel.SetActive (true);
		} else if (panel.activeSelf == true) {
			panel.SetActive(false);
		}
	}
}
