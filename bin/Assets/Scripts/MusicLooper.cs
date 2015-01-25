using UnityEngine;
using System.Collections;

public class MusicLooper : MonoBehaviour {
	[SerializeField] private double loopStartTime = 0.0f;
	private float sampleLength = 0.0f;

	// Use this for initialization
	void Start () {
		sampleLength = audio.clip.length;
	}
	
	// Update is called once per frame
	void Update () {
		if (audio.isPlaying && audio.time >= sampleLength) {
			audio.Stop();
			audio.time = (float) loopStartTime;
			audio.Play();
		}
	}
}
