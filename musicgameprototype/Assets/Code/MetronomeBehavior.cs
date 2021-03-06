﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * This class keeps track of the current BPM (beats per minute)
 */

public class MetronomeBehavior : MonoBehaviour {

	[SerializeField] private float bpm;
	[SerializeField] private AudioClip tick;

	[SerializeField] public bool playTick;
	[SerializeField] public Text bpmText; // reference to HUD object to show BPM to player
	
	[SerializeField] public bool timeSoundWithMetronome;

	private AudioSource tickSource;
	private float secToNextBeat; // seconds to next beat
	private float beatInterval;
	private bool activated; // when the metronome is not activated, it doesn't keep track of beats
	private int beat;


	// Use this for initialization
	void Start () {
		tickSource = gameObject.GetComponent<AudioSource>();
		tickSource.clip = tick;

		beatInterval = 60f / bpm;
		secToNextBeat = beatInterval;

		activated = false;
		beat = 0;
	}
	
	// Update is called once per frame
	void Update () {

		bpmText.text = "BPM: " + ((int)bpm).ToString();

		if (activated) { // tick tock
			secToNextBeat -= Time.deltaTime;
			
			if (secToNextBeat <= 0) {
				beat++;
				secToNextBeat += beatInterval;
				if (playTick)
					tickSource.PlayOneShot (tick);
			}
		} else {
			secToNextBeat = beatInterval;
		}
	}
	
	public float getSecToNextBeat() {
		return secToNextBeat;
	}
	
	public float getBPM() {
		return bpm;
	}

	public void setPlayTick(bool p) {
		playTick = p;
	}
	
	public void startPlayTick() {
		playTick = true;
	}
	
	public void stopPlayTick() {
		playTick = false;
	}

	public bool isTimeWithMetronome() {
		return timeSoundWithMetronome;
	}

	public void setActivated(bool a) {
		activated = a;
	}
	
	public bool isActivated() {
		return activated;
	}

	public int getCurrentBeat() {
		return beat;
	}

	public void setCurrentBeat(int b) {
		beat = b;
	}

}
