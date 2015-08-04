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
	//private AudioSource drums2;
	private AudioSource cymbals1;
	//private AudioSource cymbals2;

	private float secToNextBeat; // seconds to next beat
	private float beatInterval;
	private bool activated;
	private int beat = 1;
	private float bpm;

	void Start()
	{
		bpm = gameObject.GetComponent<MetronomeBehavior>().getBPM();
		activated = false;
		beatInterval = 60f / bpm;
		secToNextBeat = beatInterval;
	}

	void Update () 
	{
		if (activated) { // tick tock
			secToNextBeat -= Time.deltaTime;
			
			if (secToNextBeat <= 0) 
			{
				if (beat > 8)
					beat = 1;
				else
					beat++;

				if (beat == 1)
				{
					cymbals1.gameObject.GetComponent<AudioSource>();
				}
				secToNextBeat += beatInterval;
				tickSource.PlayOneShot(tick);
			}
		}
	}
}