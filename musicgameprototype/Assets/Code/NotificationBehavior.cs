using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationBehavior : MonoBehaviour {


	[SerializeField] public float secondsOnScreen;
	[SerializeField] public float secondsToFade;

	private float counter, fadeSpeed;
	private bool startFade;
	private Text text;

	// Use this for initialization
	void Start () {
		counter = 0f;
		startFade = false;
		text = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;

		if (counter > secondsOnScreen)
			startFade = true;

		if (startFade) {
			Color c = text.color;
			c.a = Mathf.Lerp(1, 0, (counter - secondsOnScreen) / secondsToFade);
			text.color = c;
		}

		if (counter >= (secondsToFade + secondsOnScreen))
			Destroy (gameObject);
	}
}
