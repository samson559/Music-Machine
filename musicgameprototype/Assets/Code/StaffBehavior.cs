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
	[SerializeField] public Sprite A_sprite; // A image
	[SerializeField] public Sprite B_sprite; // B image
	[SerializeField] public Sprite C_sprite; // C image
	[SerializeField] public Sprite D_sprite; // D image
	[SerializeField] public Sprite E_sprite; // E image
	[SerializeField] public Sprite F_sprite; // F image
	[SerializeField] public Sprite G_sprite; // G image
	[SerializeField] public GameObject noteName; // the prefab that contains the A,B,C, etc. image
	[SerializeField] public Sprite sprite_3; // top time signature number (the "3" in "3/4" time)
	[SerializeField] public Sprite sprite_4; // bottom time signature number (the "4" in "3/4" time)
	[SerializeField] public TextAsset composition;
	[SerializeField] public GameObject note;
	[SerializeField] public GameObject measureBar;

	private int counter;

	public float speed; // speed at which the playhead moves
	public bool playing, compositionLoaded, firstNoteHit; // is the staff playing music?
	public float playheadOffset, playheadLimit, playheadRange; // offset from screen center, make room for time signature, clef
	private GameObject[] noteArray; // array of notes which are loaded from txt file
	private GameObject[] noteNameArray; // array of note names, which rise and fade
	private int page, totalPages; // current page of composition;
	private float minX, maxX, rightLimit, leftLimit;
	private RectTransform staffTransform, phTransform, tstopTransform, tsbottomTransform, tcTransform;
	private GameObject[] measureBarArray; // array of measure bars;
	private float noteSpacing; // spacing between notes


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

		staffTransform.SetParent (GameObject.FindGameObjectWithTag ("HUD").GetComponent<RectTransform> ());
		phTransform.SetParent (staffTransform);
		tstopTransform.parent.SetParent (staffTransform);
		tsbottomTransform.parent.SetParent (staffTransform);
		tcTransform.parent.SetParent (staffTransform);

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
		noteSpacing = (playheadRange / (timesig_top * measuresDisplayed));
		
		// load measure bars
		
		measureBarArray = new GameObject[measuresDisplayed];
		loadMeasureBars ();

		string[] lines = composition.ToString().Split('\n');
		noteArray = new GameObject[lines.Length];
		noteNameArray = new GameObject[lines.Length];
		float totalBeats = 0;
		int currentPage = 1;

		// load each note
		for(int i = 0; i < noteArray.Length; i++) {
			string[] values = lines[i].Split(',');
			noteArray[i] = Instantiate(note, Vector3.zero, Quaternion.identity) as GameObject;
			noteArray[i].transform.SetParent(staff.GetComponent<RectTransform>());
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

			nRTransform.position = new Vector3(notePos.x, notePos.y, notePos.z);
			noteArray[i].GetComponent<StaffNoteData>().setOrigin(nRTransform.position);

			noteNameArray[i] = Instantiate(noteName, Vector3.zero, Quaternion.identity) as GameObject;
			Vector3 initPos = new Vector3(notePos.x, 100f, notePos.z);
			ShowNoteName snn = noteNameArray[i].GetComponent<ShowNoteName> ();
			RectTransform nnTrans = noteNameArray[i].GetComponent<RectTransform> ();
			Image nnImage = noteNameArray[i].GetComponent<Image> ();
			nnTrans.position = initPos;
			snn.setInitPos(initPos);
			snn.setActivateOnBeat(Mathf.FloorToInt(totalBeats));

			char[] valCharArray = values[0].ToCharArray();
			switch(valCharArray[0]) {
			case 'A':
				nnImage.sprite = A_sprite;
				break;
			case 'B':
				nnImage.sprite = B_sprite;
				break;
			case 'C':
				nnImage.sprite = C_sprite;
				break;
			case 'D':
				nnImage.sprite = D_sprite;
				break;
			case 'E':
				nnImage.sprite = E_sprite;
				break;
			case 'F':
				nnImage.sprite = F_sprite;
				break;
			case 'G':
				nnImage.sprite = G_sprite;
				break;
			}
			
			totalBeats += snd.getBeatsPlayed();
		}

		page = 0;
		totalPages = Mathf.FloorToInt (calcBeatsInCompotion(noteArray) / (timesig_top * measuresDisplayed));

		compositionLoaded = true;
	}

	private void loadMeasureBars() {
		for (int i = 0; i < measureBarArray.Length; i++) {
			measureBarArray[i] = Instantiate(measureBar, Vector3.zero, Quaternion.identity) as GameObject;
			float xPos = (i + 1) * (playheadRange/measuresDisplayed) + (noteSpacing * 1.1f);
			Transform mTransform = measureBarArray[i].GetComponent<RectTransform>();
			mTransform.SetParent(staff.GetComponent<RectTransform>());
			mTransform.position = new Vector3(xPos, 50, mTransform.position.z);
		}
	}

	public void setPlaying(bool play) {
		playing = play;
	}
	
	public bool isPlaying() {
		return playing;
	}

	public void startPlaying() {
		//playing = true; // 'playing' is now only set to true after firstNoteHit is true

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
		firstNoteHit = false;

		// deactivate metronome
		MetronomeBehavior mb = GameObject.FindGameObjectWithTag("Metronome").GetComponent<MetronomeBehavior> ();
		mb.setActivated(false);
		mb.setCurrentBeat (0);

		//reset all notes on the staff to their original positions
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

	public void noteHit(string _notename) { // this is called each time a musicObj is struck by the marble
		if (_notename == null)
			_notename = " ";
		MetronomeBehavior mb = GameObject.FindGameObjectWithTag("Metronome").GetComponent<MetronomeBehavior> ();

		if (!firstNoteHit) {
			firstNoteHit = true;
			playing = true;

			mb.setActivated(true);
		}

		int beat = mb.getCurrentBeat();

		// check if the right note is hit at the right time
		GameObject currentNote = getNoteAtBeat (beat);
		
		// this happens when the marker is past the end of the song, and there are no more notes in the composition
		if (currentNote == null)
			return;

		string targetNote = currentNote.GetComponent<StaffNoteData> ().getName ();

		if (targetNote == _notename)
			currentNote.GetComponent<Image> ().color = Color.green;
		else
			currentNote.GetComponent<Image> ().color = Color.red;

		Debug.Log ("target note: " + targetNote + ", note hit: " + _notename);
		/*
		foreach (GameObject note in noteArray)
			note.GetComponent<StaffNoteData> ().checkNote (_notename);
		*/
	}

	public GameObject getNoteAtBeat(int beat) {
		int beatIndex = 0;

		for (int i = 0; i < noteArray.Length; i++) {
			StaffNoteData snd = noteArray[i].GetComponent<StaffNoteData>();

			if(beatIndex == beat) {
				return noteArray[i];
			}

			beatIndex += Mathf.FloorToInt(snd.beatsPlayed); // fyi this doesn't work on beat 1.5 or any fractional beat...

			if(beatIndex > beat)
				return null;
		}

		return noteName;
	}


	public GameObject[] getNoteArray()
	{
		return noteArray;
	}

}