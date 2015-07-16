using UnityEngine;
using System.Collections;

public class MusicObj : MonoBehaviour {
	[SerializeField] private AudioClip mySound;
	private AudioSource src;
	// Use this for initialization
	void Start () {
		src = gameObject.GetComponent<AudioSource>();
		src.clip = mySound;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		// this code makes sure the sound only plays on the beat
		GameObject metronome = GameObject.Find ("Metronome");
		float delay = ((MetronomeBehavior)metronome.GetComponent<MetronomeBehavior>()).getSecToNextBeat();
		src.PlayDelayed (delay);
		//src.PlayOneShot (mySound);
	}
}
