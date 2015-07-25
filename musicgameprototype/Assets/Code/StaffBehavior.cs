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
	[SerializeField] public Canvas canvas; // canvas object (from HUD)
	[SerializeField] public GameObject playhead; // playhead object
	[SerializeField] public GameObject staff; // staff object
	[SerializeField] public Image timesig_top_image; // top time signature number (the "3" in "3/4" time)
	[SerializeField] public Image timesig_bottom_image; // bottom time signature number (the "4" in "3/4" time)
	[SerializeField] public Sprite sprite_3; // top time signature number (the "3" in "3/4" time)
	[SerializeField] public Sprite sprite_4; // bottom time signature number (the "4" in "3/4" time)
	[SerializeField] public TextAsset composition;
	[SerializeField] public GameObject note;

	private int counter;

	public float speed; // speed at which the playhead moves
	public bool playing, compositionLoaded; // is the staff playing music?
	public float playheadOffset, playheadLimit; // offset from screen center, make room for time signature, clef
	private GameObject[] noteArray; // array of notes which are loaded from txt file
	private int page, totalPages; // current page of composition;
	private float minX, maxX, rightLimit, leftLimit;
	private RectTransform staffTransform, phTransform, tstopTransform, tbottomTransform;

	// Use this for initialization
	void Start () {
		playing = false;

		counter = 0;
		page = 0;
		totalPages = 0;
		compositionLoaded = false;

		staffTransform = staff.GetComponent<RectTransform>();
		phTransform = playhead.GetComponent<RectTransform>();
		tstopTransform = timesig_top_image.GetComponent<RectTransform> ();
		tbottomTransform = timesig_bottom_image.GetComponent<RectTransform>();

		staffTransform.parent = GameObject.FindGameObjectWithTag ("HUD").GetComponent<RectTransform> ();
		phTransform.parent = staffTransform;
		tstopTransform.parent = staffTransform;
		tbottomTransform.parent = staffTransform;

		leftLimit = 100;
		rightLimit = 100;
		
		minX = 100;
		maxX = (staffTransform.rect.width * canvas.scaleFactor) - rightLimit;

		phTransform.position = new Vector3 (minX, phTransform.position.y, phTransform.position.z);
		tstopTransform.position = new Vector3 (minX - 40, tstopTransform.position.y, tstopTransform.position.z);
		tbottomTransform.position = new Vector3 (minX - 40, tbottomTransform.position.y, tbottomTransform.position.z);

		speed = 0f;

		loadComposition ();
	}
	
	// Update is called once per frame
	void Update () {
		
		RectTransform staffTransform = staff.GetComponent<RectTransform>() as RectTransform;
		RectTransform phTransform = playhead.GetComponent<RectTransform>() as RectTransform;
		
		// update time signature sprites, with 4 being the default
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
		
		
		Vector3 playheadOrigin = phTransform.transform.position;
		
		speed = 0f;
		
		if (playing && compositionLoaded) { // only scroll
			GameObject metronome = GameObject.Find ("Metronome");
			
			// calculate speed
			float bpm = ((MetronomeBehavior)metronome.GetComponent<MetronomeBehavior> ()).getBPM ();
			float playheadRange = maxX - minX;
			//float distPerBeat = playheadRange / (timesig_top * measuresDisplayed);
			//float distToNextBeat = (Mathf.Floor(phTransform.position.x/playheadRange) * distPerBeat);
			float scrollScreenTime = (60 * measuresDisplayed * timesig_top) / bpm; // the 60 converts from beats per minute to beats per second
			speed = playheadRange / scrollScreenTime;
			//speed = distToNextBeat/metronome.GetComponent<MetronomeBehavior>().getSecToNextBeat();

			if(phTransform.position.x >= maxX) phTransform.position = new Vector3 (minX, playheadOrigin.y, playheadOrigin.z);
			else phTransform.position = new Vector3 (playheadOrigin.x + (speed * Time.deltaTime), playheadOrigin.y, playheadOrigin.z);
		}
		
	}

	public void loadComposition() {

		string[] lines = composition.ToString().Split('\n');
		noteArray = new GameObject[lines.Length];
		float totalBeats = 0;
		int currentPage = 1;

		// load each note
		for(int i = 0; i < noteArray.Length; i++) {
			string[] values = lines[i].Split(',');
			noteArray[i] = Instantiate(note, Vector3.zero, Quaternion.identity) as GameObject;
			noteArray[i].transform.parent = staff.GetComponent<RectTransform>();
			StaffNoteData snd = noteArray[i].GetComponent<StaffNoteData>();
			snd.setName(values[0]);
			snd.setBeatsPlayed(float.Parse(values[1]));

			RectTransform nRTransform = noteArray[i].GetComponent<RectTransform>();
			Vector3 notePos = nRTransform.position;

			float noteYBase = 6;
			float noteYSpacing = 9;

			switch(values[0]) {
			case "C3":
				notePos.y = noteYBase + 0 * noteYSpacing;
				break;
			case "D3":
				notePos.y = noteYBase + 1 * noteYSpacing;
				break;
			case "E3":
				notePos.y = noteYBase + 2 * noteYSpacing;
				break;
			case "F3":
				notePos.y = noteYBase + 3 * noteYSpacing;
				break;
			case "G3":
				notePos.y = noteYBase + 4 * noteYSpacing;
				break;
			case "A3":
				notePos.y = noteYBase + 5 * noteYSpacing;
				break;
			case "B3":
				notePos.y = noteYBase + 6 * noteYSpacing;
				break;
			case "C4":
				notePos.y = noteYBase + 7 * noteYSpacing;
				break;
			}


			snd.setPage(currentPage);
			if((totalBeats / measuresDisplayed * timesig_top) > currentPage) currentPage ++; 

			float playheadRange = maxX - minX;
			float noteSpacing = (playheadRange / (timesig_top * measuresDisplayed));
			float noteXOffset = totalBeats * noteSpacing;
			notePos.x += minX + noteXOffset;
			totalBeats += snd.getBeatsPlayed();


			nRTransform.position = new Vector3(notePos.x, notePos.y, notePos.z);
		}

		page = 0;
		totalPages = Mathf.FloorToInt (calcBeatsInCompotion(noteArray) / (timesig_top * measuresDisplayed));

		Debug.Log (totalPages);

		compositionLoaded = true;
	}

	public void setPlaying(bool play) {
		playing = play;
	}
	
	public bool isPlaying() {
		return playing;
	}

	public void startPlaying() {
		playing = true;
	}

	public void stopPlaying() {
		playing = false;
	}

	private float calcBeatsInCompotion(GameObject[] array) {
		float totalBeats = 0;

		for (int i = 0; i < array.Length; i++) {
			StaffNoteData snd = array[i].GetComponent<StaffNoteData>();
			totalBeats += snd.getBeatsPlayed();
		}
		return totalBeats;
	}

	public void resetStaff() {
		page = 1; // go back to first page

		// reset playhead position
		Transform phTransform = playhead.GetComponent<Transform> ();
		Vector3 playheadOrigin = new Vector3 (phTransform.position.x, phTransform.position.y, phTransform.position.z);
		phTransform.position = new Vector3 (minX, playheadOrigin.y, playheadOrigin.z);

		// stop tick sound
		playing = false;
	}
}