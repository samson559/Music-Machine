using UnityEngine;
using System.Collections;

public class MusicObj : MonoBehaviour {
	[SerializeField] private AudioClip mySound;
	private AudioSource src;
	private string noteName; //name of the note that will play on hit
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
		StaffBehavior staffBhv = GameObject.FindGameObjectWithTag ("Staff").GetComponent<StaffBehavior> ();
		//staffBhv.noteHit (noteName);
		
		MetronomeBehavior metronome = GameObject.Find ("Metronome").GetComponent<MetronomeBehavior>();
		if (metronome.isTimeWithMetronome()) {
			// this code makes sure the sound only plays on the beat
			float delay = metronome.getSecToNextBeat ();
			src.PlayDelayed (delay);
		} else {
			src.PlayOneShot (mySound);
		}
	}
}
