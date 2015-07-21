using UnityEngine;
using System.Collections;

public class PlayheadDriver : MonoBehaviour {
	private bool play;
	[SerializeField] private float songlength = 0;
	[SerializeField] private MetronomeBehavior metro;
	private RectTransform mytransform;
	// Use this for initialization
	void Start () {
		mytransform = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (play) {
			float amountToMove = metro.getBPM () * Time.deltaTime;
			mytransform.position = new Vector3 (mytransform.position.x + amountToMove, mytransform.position.y, mytransform.position.z);
		}
	}
	public void setPlay(bool _play)
	{
		play = _play;
	}
	public void resetPlayhead()
	{
		mytransform.position = new Vector3 (0, mytransform.position.y, mytransform.position.z);
	}
}
