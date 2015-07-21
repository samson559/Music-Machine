using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * This class controls behavior for the staff. It contains an array of notes to play and controls staff graphical behavior, such as a moving playhead in time with notes.
 * playhead = triangle at the the top of the staff
 */

public class StaffBehavior : MonoBehaviour {
	
	[SerializeField] public int measuresDisplayed; // how many measures dispalyed on the screen
	[SerializeField] public int timesig_top; // top time signature number (the "3" in "3/4" time)
	[SerializeField] public int timesig_bottom; // bottom time signature number (the "4" in "3/4" time)
	[SerializeField] public Image timesig_top_image; // top time signature number (the "3" in "3/4" time)
	[SerializeField] public Image timesig_bottom_image; // bottom time signature number (the "4" in "3/4" time)
	[SerializeField] public Sprite sprite_3; // top time signature number (the "3" in "3/4" time)
	[SerializeField] public Sprite sprite_4; // bottom time signature number (the "4" in "3/4" time)
	[SerializeField] public Vector3 playheadPosit; // position of the playhead;

	public float speed; // speed at which the playhead moves
	public bool playing; // is the staff playing music?
	public float playheadOffset; // pixels offset, make room for time signature, clef

	// Use this for initialization
	void Start () {
		playheadOffset = 100f; // test value
		//x: -366.9
		playheadPosit = new Vector3 ();
		playing = false;
		speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		// update time signature sprites
		switch(timesig_top) {
		case 3:
			timesig_top_image.sprite = sprite_3;
			break;
		case 4:
			timesig_top_image.sprite = sprite_4;
			break;
		default:
			timesig_top_image.sprite = sprite_4;
			break;
		}

		switch(timesig_bottom) {
		case 3:
			timesig_bottom_image.sprite = sprite_3;
			break;
		case 4:
			timesig_bottom_image.sprite = sprite_4;
			break;
		default:
			timesig_bottom_image.sprite = sprite_4;
			break;
		}


		if (playing) {
			GameObject metronome = GameObject.Find ("Metronome");
			//float timeToNextBeat = ((MetronomeBehavior)metronome.GetComponent<MetronomeBehavior>()).getSecToNextBeat();
			float bpm = ((MetronomeBehavior)metronome.GetComponent<MetronomeBehavior>()).getBPM();

			// e.x.: 4/4 signature, displaying 2 measures at a time, at 160 bpm
			// there would be a total of 8 notes, which the playhead would need to clear in 8 beats, which would take 8/160 of a minute to clear or 3 seconds.
			// in 3 seconds, the playhead would need to travel (Screen.width - playheadOffset) / 3 seconds
			float playheadRange = Screen.width - playheadOffset;
			float scrollScreenTime = (60 * measuresDisplayed * timesig_bottom) / bpm;
			speed = playheadRange / scrollScreenTime;
			Vector3 playheadOrigin = playheadPosit;
			playheadPosit = new Vector3(playheadOrigin.x + speed * Time.deltaTime, playheadOrigin.y, playheadOrigin.z);
		}

	}

	public void setPlaying(bool play) {
		playing = play;
	}

	public bool isPlaying() {
		return playing;
	}
}
