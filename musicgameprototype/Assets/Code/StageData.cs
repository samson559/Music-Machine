using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * This class keeps track of information about the stage, such as its height and width
 * - Nathaniel
 */ 

public class StageData : MonoBehaviour {

	const int EASY_MODE = 0;
	const int NORMAL_MODE = 1;
	const int HARD_MODE = 2;

	[SerializeField] float stageWidth;
	[SerializeField] float stageHeight;
	[SerializeField] Slider difficultySlider;
	[SerializeField] Text difficultyText;
	private int difficulty;
	private StaffBehavior staffBehavior;

	// Use this for initialization
	void Start () {
		difficulty = Mathf.RoundToInt(difficultySlider.value);
		staffBehavior = GameObject.FindObjectOfType<StaffBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float getStageWidth() {
		return stageWidth;
	}
	
	public float getStageHeight() {
		return stageHeight;
	}

	public int getDifficulty() {
		return difficulty;
	}
	
	public void setDifficulty() {
		difficulty = Mathf.RoundToInt(difficultySlider.value);

		string text = "Difficulty:\n";
		if (difficulty == 0f)
			text += "Easy";
		else if (difficulty == 1f)
			text += "Normal";
		else if (difficulty == 2f)
			text += "Hard";
		else
			text += "[ERROR]";

		difficultyText.text = text;

		if (difficulty == 0)
			staffBehavior.colorizeNotes ();
		else
			staffBehavior.resetNoteColors ();
	}
}
