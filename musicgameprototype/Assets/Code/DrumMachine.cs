using UnityEngine;
using System.Collections;

//Code for drum machine functionality in the game. At the time, still needs some graphical representation.

public class DrumMachine : MonoBehaviour 
{
	
	[SerializeField] private AudioClip hihat;
	[SerializeField] private AudioClip crash1;
	[SerializeField] private AudioClip crash2;
	[SerializeField] private AudioClip ride;
	[SerializeField] private AudioClip snare;
	[SerializeField] private AudioClip hitom;
	[SerializeField] private AudioClip midtom;
	[SerializeField] private AudioClip lowtom;
	[SerializeField] private AudioClip kick;

	private AudioSource drums1;
	private AudioSource drums2;
	private AudioSource cymbals1;
	private AudioSource cymbals2;
	private bool activated = false;
	private int beat = 1;
	private float bpm;

	void Awake()
	{
		bpm = gameObject.GetComponent<MetronomeBehavior>().getBPM();
	}

	void Update () 
	{
		
		bpmText.text = "BPM: " + ((int)bpm).ToString();
		
		if (activated) { // tick tock
			secToNextBeat -= Time.deltaTime;
			
			if (secToNextBeat <= 0) {
				beat++;
				secToNextBeat += beatInterval;
				if(playTick) tickSource.PlayOneShot(tick);
			}
		}
	}
}