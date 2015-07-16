using UnityEngine;
using System.Collections;

/*
 * This class keeps track of the current BPM (beats per minute)
 */

public class MetronomeBehavior : MonoBehaviour {

	[SerializeField] float bpm;
	[SerializeField] private AudioClip tick;

	private AudioSource tickSource;
	private float secToNextBeat; // seconds to next beat
	private float beatInterval;
	private bool playTick;

	// Use this for initialization
	void Start () {
		playTick = true;
		tickSource = gameObject.GetComponent<AudioSource>();
		tickSource.clip = tick;

		beatInterval = 60f / bpm;
		secToNextBeat = beatInterval;
	}
	
	// Update is called once per frame
	void Update () {
		float timeScale = Time.timeScale; // if the game is paused, timeScale will equal 0
		secToNextBeat -= Time.deltaTime * timeScale;
		Debug.Log (secToNextBeat);

		if (secToNextBeat <= 0) {
			secToNextBeat += beatInterval;
			if(playTick) tickSource.PlayOneShot(tick);
		}
	}

	public float getSecToNextBeat() {
		return secToNextBeat;
	}

	public void isPlayTick(bool p) {
		playTick = p;
	}

}
