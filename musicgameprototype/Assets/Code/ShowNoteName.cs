using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowNoteName : MonoBehaviour {

	private Image image;
	private RectTransform rTrans;
	private int activateOnBeat;
	private bool active;
	private Vector3 initPos;
	private float riseSpeed, counter, secOnScreen;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		rTrans = GetComponent<RectTransform> ();
		image.canvasRenderer.SetAlpha (0);

		secOnScreen = 1.5f;
		riseSpeed = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		MetronomeBehavior mb = GameObject.FindGameObjectWithTag ("Metronome").GetComponent<MetronomeBehavior> ();

		if (mb.getCurrentBeat () >= activateOnBeat) {
			active = true;
			image.canvasRenderer.SetAlpha (1);
			counter = 0f;
		}

		if (active) {
			counter += Time.deltaTime;
			float alpha = Mathf.Lerp(1, 0, counter / secOnScreen);
			image.canvasRenderer.SetAlpha(alpha);
			Vector3 move = new Vector3(rTrans.position.x, rTrans.position.y + riseSpeed * Time.deltaTime, rTrans.position.z);
			rTrans.transform.position = move;

			if(alpha <= 0 || !mb.isActivated()) {
				image.canvasRenderer.SetAlpha(0f);
				active = false;
				rTrans.transform.position = new Vector3(initPos.x, initPos.y, initPos.z);
			}
		}
	}

	public void setActivateOnBeat(int b) {
		activateOnBeat = b;
	}

	public void setInitPos(Vector3 v) {
		initPos = v;
	}
}
