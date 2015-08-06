using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowNoteName : MonoBehaviour {

	private Image image;
	private RectTransform rTrans;
	private float activateOnBeat;
	private bool active;
	private Vector3 initPos;
	private float riseSpeed, counter, secOnScreen;
	private StageData stageData;

	// Use this for initialization
	void Start () {
		active = false;

		image = GetComponent<Image> ();
		rTrans = GetComponent<RectTransform> ();
		image.canvasRenderer.SetAlpha (0);

		secOnScreen = 1.5f;
		riseSpeed = 3f;

		stageData = GameObject.FindObjectOfType<StageData> ();
	}
	
	// Update is called once per frame
	void Update () {
		MetronomeBehavior mb = GameObject.FindGameObjectWithTag ("Metronome").GetComponent<MetronomeBehavior> ();

		if (!active && mb.isActivated() && mb.getCurrentBeat () >= activateOnBeat && stageData.getDifficulty () <= 1)
			activate();

		if (active) {
			counter += Time.deltaTime;
			float alpha = Mathf.Lerp(1, 0, counter / secOnScreen);
			image.canvasRenderer.SetAlpha(alpha);
			Vector3 move = new Vector3(rTrans.position.x, rTrans.position.y + riseSpeed * Time.deltaTime, rTrans.position.z);
			rTrans.transform.position = move;
		}
	}

	public void activate() {
		active = true;
		image.canvasRenderer.SetAlpha (1);
	}

	public void reset() {
		active = false;
		image.canvasRenderer.SetAlpha(0f);
		rTrans.transform.position = new Vector3(initPos.x, initPos.y, initPos.z);
		counter = 0;
	}

	public void setActivateOnBeat(float b) {
		activateOnBeat = b;
	}

	public void setInitPos(Vector3 v) {
		initPos = v;
	}
}
