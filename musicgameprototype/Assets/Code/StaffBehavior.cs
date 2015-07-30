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
	[SerializeField] public Image treble_clef; // treble clef image
	[SerializeField] public Image measure_bar; // measure bar image
	[SerializeField] public Sprite sprite_3; // top time signature number (the "3" in "3/4" time)
	[SerializeField] public Sprite sprite_4; // bottom time signature number (the "4" in "3/4" time)
	[SerializeField] public TextAsset composition;
	[SerializeField] public GameObject note;
	[SerializeField] public GameObject measureBar;

	private int counter;

	public float speed; // speed at which the playhead moves
	public bool playing, compositionLoaded; // is the staff playing music?
	public float playheadOffset, playheadLimit, playheadRange; // offset from screen center, make room for time signature, clef
	private GameObject[] noteArray; // array of notes which are loaded from txt file
	private int page, totalPages; // current page of composition;
	private float minX, maxX, rightLimit, leftLimit;
	private RectTransform staffTransform, phTransform, tstopTransform, tsbottomTransform, tcTransform;
	private GameObject[] measureBarArray; // array of measure bars;

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
		tsbottomTransform = timesig_bottom_image.GetComponent<RectTransform>();
		tcTransform = treble_clef.GetComponent<RectTransform> ();

		staffTransform.parent = GameObject.FindGameObjectWithTag ("HUD").GetComponent<RectTransform> ();
		phTransform.parent = staffTransform;
		tstopTransform.parent = staffTransform;
		tsbottomTransform.parent = staffTransform;
		tcTransform.parent = staffTransform;

		leftLimit = 100;
		rightLimit = 100;
		
		minX = leftLimit;
		maxX = (staffTransform.rect.width * canvas.scaleFactor) - rightLimit;
		playheadRange = maxX - minX;

		phTransform.position = new Vector3 (minX, phTransform.position.y, phTransform.position.z);
		tstopTransform.position = new Vector3 (minX - 40, tstopTransform.position.y, tstopTransform.position.z);
		tsbottomTransform.position = new Vector3 (minX - 40, tsbottomTransform.position.y, tsbottomTransform.position.z);
		tcTransform.position = new Vector3 (minX - 80, tcTransform.position.y, tcTransform.position.z);

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
			//float distPerBeat = playheadRange / (timesig_top * measuresDisplayed);
			//float distToNextBeat = (Mathf.Floor(phTransform.position.x/playheadRange) * distPerBeat);
			float scrollScreenTime = (60 * measuresDisplayed * timesig_top) / bpm; // the 60 converts from beats per minute to beats per second
			speed = playheadRange / scrollScreenTime;
			//speed = distToNextBeat/metronome.GetComponent<MetronomeBehavior>().getSecToNextBeat();

			if(phTransform.position.x >= maxX) {
				phTransform.position = new Vector3 (minX, playheadOrigin.y, playheadOrigin.z);
				page++;
				shiftNotes(1);
				Debug.Log ("page: " + page);
				Debug.Log ("totalPages: " + totalPages);
				if(page >= totalPages) resetStaff();
			} else phTransform.position = new Vector3 (playheadOrigin.x + (speed * Time.deltaTime), playheadOrigin.y, playheadOrigin.z);
		}
		
	}

	private void shiftNotes(int pagesToShift) {
		for (int i = 0; i < noteArray.Length; i++) {
			Transform nTransform = noteArray[i].GetComponent<Transform>();
			Vector3 origin = nTransform.position;
			Vector3 newPos = new Vector3(origin.x - (pagesToShift * playheadRange), origin.y, origin.z);
			nTransform.position = newPos;
		}
	}

	public void loadComposition() {
		float noteSpacing = (playheadRange / (timesig_top * measuresDisplayed));
		
		// load measure bars
		measureBarArray = new GameObject[measuresDisplayed];
		for (int i = 0; i < measureBarArray.Length; i++) {
			measureBarArray[i] = Instantiate(measureBar, Vector3.zero, Quaternion.identity) as GameObject;
			float xPos = (i + 1) * (playheadRange/measuresDisplayed) + (noteSpacing * 1.1f);
			Transform mTransform = measureBarArray[i].GetComponent<RectTransform>();
			mTransform.parent = staff.GetComponent<RectTransform>();
			mTransform.position = new Vector3(xPos, 50, mTransform.position.z);
		}

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

			float noteYBase = 20;
			float noteYSpacing = 7.3f;

			switch(values[0]) {
			case "C3":
				notePos.y = noteYBase - 7 * noteYSpacing;
				break;
			case "D3":
				notePos.y = noteYBase - 6 * noteYSpacing;
				break;
			case "E3":
				notePos.y = noteYBase - 5 * noteYSpacing;
				break;
			case "F3":
				notePos.y = noteYBase - 4 * noteYSpacing;
				break;
			case "G3":
				notePos.y = noteYBase - 3 * noteYSpacing;
				break;
			case "A3":
				notePos.y = noteYBase - 2 * noteYSpacing;
				break;
			case "B3":
				notePos.y = noteYBase - 1 * noteYSpacing;
				break;
			case "C4":
				notePos.y = noteYBase + 0 * noteYSpacing;
				break;
			case "D4":
				notePos.y = noteYBase + 1 * noteYSpacing;
				break;
			case "E4":
				notePos.y = noteYBase + 2 * noteYSpacing;
				break;
			case "F4":
				notePos.y = noteYBase + 3 * noteYSpacing;
				break;
			case "G4":
				notePos.y = noteYBase + 4 * noteYSpacing;
				break;
			case "A4":
				notePos.y = noteYBase + 5 * noteYSpacing;
				break;
			case "B4":
				notePos.y = noteYBase + 6 * noteYSpacing;
				break;
			case "C5":
				notePos.y = noteYBase + 7 * noteYSpacing;
				break;
			case "D5":
				notePos.y = noteYBase + 8 * noteYSpacing;
				break;
			case "E5":
				notePos.y = noteYBase + 9 * noteYSpacing;
				break;
			case "F5":
				notePos.y = noteYBase + 10 * noteYSpacing;
				break;
			case "G5":
				notePos.y = noteYBase + 11 * noteYSpacing;
				break;
			case "A5":
				notePos.y = noteYBase + 12 * noteYSpacing;
				break;
			case "B5":
				notePos.y = noteYBase + 13 * noteYSpacing;
				break;
			}


			snd.setPage(currentPage);
			if((totalBeats / measuresDisplayed * timesig_top) > currentPage) currentPage ++; 

			float noteXOffset = totalBeats * noteSpacing;
			notePos.x += minX + noteXOffset;
			totalBeats += snd.getBeatsPlayed();

			nRTransform.position = new Vector3(notePos.x, notePos.y, notePos.z);
			noteArray[i].GetComponent<StaffNoteData>().setOrigin(nRTransform.position);
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

		if (Time.timeScale < 1)
			Time.timeScale = 1;

		// restart marbles
		GameObject[] marbleArray = GameObject.FindGameObjectsWithTag ("Marble");
		for (int i = 0; i < marbleArray.Length; i++) {
			Rigidbody2D mRigidBody = marbleArray[i].GetComponent<Rigidbody2D> ();
			MarbleBehavior mBehavior = marbleArray[i].GetComponent<MarbleBehavior> ();
			mRigidBody.isKinematic = false;
			mRigidBody.velocity = mBehavior.getSavedVelocity();
		}

		// spawn marbles (only if possible!)
		GameObject[] spawnerArray = GameObject.FindGameObjectsWithTag ("MarbleSpawner");
		for (int i = 0; i < spawnerArray.Length; i++) {
			spawnerArray[i].GetComponent<MarbleSpawnBehavior>().spawnMarble();
		}
	}

	public void stopPlaying() {
		playing = false;

		// pause marbles
		GameObject[] marbleArray = GameObject.FindGameObjectsWithTag ("Marble");
		for (int i = 0; i < marbleArray.Length; i++) {
			Rigidbody2D mRigidBody = marbleArray[i].GetComponent<Rigidbody2D> ();
			MarbleBehavior mBehavior = marbleArray[i].GetComponent<MarbleBehavior> ();
			mBehavior.setSavedVelocity(mRigidBody.velocity);
			mRigidBody.isKinematic = true;
		}
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
		page = 0; // go back to first page

		//reset all notes to their original positions
		for (int i = 0; i < noteArray.Length; i++) {
			Transform nTransform = noteArray[i].GetComponent<Transform>();
			Vector3 origin = noteArray[i].GetComponent<StaffNoteData>().getOrigin();
			nTransform.position = origin;
		}

		// reset playhead position
		Transform phTransform = playhead.GetComponent<Transform> ();
		Vector3 playheadOrigin = new Vector3 (phTransform.position.x, phTransform.position.y, phTransform.position.z);
		phTransform.position = new Vector3 (minX, playheadOrigin.y, playheadOrigin.z);

		// stop tick sound, scrolling
		playing = false;

		// destroy all marbles in scene
		GameObject[] marbleArray = GameObject.FindGameObjectsWithTag ("Marble");
		for (int i = 0; i < marbleArray.Length; i++) {
			GameObject.Destroy(marbleArray[i]);
		}
		
		GameObject[] spawnerArray = GameObject.FindGameObjectsWithTag ("MarbleSpawner");
		for (int i = 0; i < spawnerArray.Length; i++) {
			spawnerArray[i].GetComponent<MarbleSpawnBehavior>().setComplete(false);
		}
	}
}