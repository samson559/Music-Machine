using UnityEngine;
using System.Collections;

public class StaffNoteData : MonoBehaviour {

	[SerializeField] public string name;
	[SerializeField] public float beatsPlayed;
	[SerializeField] public int page;
	
	public StaffNoteData(string name, int bp, int page) {
		this.name = name;
		beatsPlayed = bp;
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
}
