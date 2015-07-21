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
	
	private int counter;
	
	public float speed; // speed at which the playhead moves
	public bool playing; // is the staff playing music?
	public float playheadOffset, playheadLimit; // offset from screen center, make room for time signature, clef
	
	private Note[] noteArray;
	
	//private Vector3 playheadPosit;
	
	private float minX, maxX, rightLimit;
	
	// Use this for initialization
	void Start () {
		playing = true;
		
		counter = 0;
		
		RectTransform staffTransform = staff.GetComponent<RectTransform>() as RectTransform;
		RectTransform phTransform = playhead.GetComponent<RectTransform>() as RectTransform;
		
		rightLimit = 150 * canvas.scaleFactor; // test value
		
		minX = phTransform.position.x;
		maxX = minX + (staffTransform.rect.width * canvas.scaleFactor) - rightLimit;
		
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
		
		if (playing) {
			GameObject metronome = GameObject.Find ("Metronome");
			
			// calculate speed
			float bpm = ((MetronomeBehavior)metronome.GetComponent<MetronomeBehavior> ()).getBPM ();
			float playheadRange = maxX - minX;
			float scrollScreenTime = (60 * measuresDisplayed * timesig_top) / bpm;
			speed = playheadRange / scrollScreenTime;
			
			if(phTransform.position.x >= maxX) phTransform.position = new Vector3 (minX, playheadOrigin.y, playheadOrigin.z);
			else phTransform.position = new Vector3 (playheadOrigin.x + (speed * Time.deltaTime), playheadOrigin.y, playheadOrigin.z);
		}
		
	}
	
	public void loadComposition() {
		
		string[] lines = composition.ToString().Split('\n');
		noteArray = new Note[lines.Length];
		
		for(int i = 0; i < noteArray.Length; i++) {
			string[] values = lines[i].Split(',');
			noteArray[i] = new Note(values[0], float.Parse(values[1].ToString()));
		}
	}
	
	public void setPlaying(bool play) {
		playing = play;
	}
	
	public bool isPlaying() {
		return playing;
	}
}


class Note {
	
	private string name;
	private float duration;
	private AudioClip clip;
	private float beatsPlayed;
	
	public Note(string name, float d) {
		this.name = name;
		this.duration = d;
		beatsPlayed = 0;
		
		switch(name) {
		case "C3":
			clip = Resources.Load("SFX/Piano/C3.wav") as AudioClip;
			break;
		case "D3":
			clip = Resources.Load("SFX/Piano/D3.wav") as AudioClip;
			break;
		case "E3":
			clip = Resources.Load("SFX/Piano/E3.wav") as AudioClip;
			break;
		case "F3":
			clip = Resources.Load("SFX/Piano/F3.wav") as AudioClip;
			break;
		case "G3":
			clip = Resources.Load("SFX/Piano/G3.wav") as AudioClip;
			break;
		case "A3":
			clip = Resources.Load("SFX/Piano/A3.wav") as AudioClip;
			break;
		case "B3":
			clip = Resources.Load("SFX/Piano/B3.wav") as AudioClip;
			break;
		case "C4":
			clip = Resources.Load("SFX/Piano/C4.wav") as AudioClip;
			break;
		}
	}
	
	public AudioClip getAudioClip() {
		return clip;
	}
	
}