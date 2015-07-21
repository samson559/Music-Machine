using UnityEngine;
using System.Collections;

/*
 * This class controls behavior for the staff. It contains an array of notes to play and controls staff graphical behavior, such as a moving marker in time with notes.
 */

public class StaffBehavior : MonoBehaviour {
	
	[SerializeField] public int measuresDisplayed; // how many measures dispalyed on the screen
	[SerializeField] public int timesig_top; // top time signature number (the "3" in "3/4" time)
	[SerializeField] public int timesig_bottom; // bottom time signature number (the "4" in "3/4" time)
	[SerializeField] public Vector3 markerPosit; // position of the marker;

	public float speed; // speed at which the marker moves
	public bool playing; // is the staff playing music?
	public float markerOffset; // pixels offset, make room for time signature, clef

	// Use this for initialization
	void Start () {
		markerOffset = 100f; // test value
		playing = false;
		speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing) {
			GameObject metronome = GameObject.Find ("Metronome");
			float timeToNextBeat = ((MetronomeBehavior)metronome.GetComponent<MetronomeBehavior>()).getSecToNextBeat();
			float markerRange = Screen.width - markerOffset;
			float beatsPerMinute = ((MetronomeBehavior)metronome.GetComponent<MetronomeBehavior>()).getBPM();
			speed = markerRange / (measuresDisplayed * timesig_bottom)
		}

	}

	public void setPlaying(bool play) {
		playing = play;
	}

	public bool isPlaying() {
		return playing;
	}
}
