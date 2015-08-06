using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StaffNoteData : MonoBehaviour {

	[SerializeField] public string name;
	[SerializeField] public float beatsPlayed;
	[SerializeField] public int page;
	 private Sprite playhead;
	private Sprite mySprite;
	void Start()
	{
		mySprite = transform.GetComponent<Image> ().sprite;
		playhead = transform.parent.GetComponent<Image> ().sprite;
	}

	private Vector3 origin;
	public StaffNoteData(string name, int bp, int page) {
		this.name = name;
		beatsPlayed = bp;
		page = page;
	}

	public void setPage(int p) {
		page = p;
	}
	
	public float getBeatsPlayed() {
		return beatsPlayed;
	}
	
	public string getName()  {
		return name;
	}

	public void setBeatsPlayed(float bp) {
		this.beatsPlayed = bp;
	}

	public void setName(string n) {
		this.name = n;
	}
	/*
	public void checkNote(string noteHit)
	{
		if (mySprite.bounds.Intersects (playhead.bounds)) {
			Debug.Log ("Playhead hit me at " + Time.realtimeSinceStartup);
			if (noteHit == name) {
				GetComponent<Image> ().color = Color.green;
			} else {
				GetComponent<Image> ().color = Color.red;
			}
		}
		/*
		if (mySprite.bounds.Intersects (playhead.bounds)) {
			Debug.Log ("Playhead hit me at " + Time.realtimeSinceStartup);
			GetComponent<Image> ().color = Color.red;
			return true;
		}
		return false;
	}
	
		*/
	public Vector3 getOrigin() {
		return origin;
	}
	
	public void setOrigin(Vector3 o) {
		origin = o;
	}
}
