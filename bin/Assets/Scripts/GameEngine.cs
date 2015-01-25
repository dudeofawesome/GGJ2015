using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	[SerializeField] private GameObject player;

	enum World {EARTH:0, MARS:1, ISS:2};
	[SerializeField] private World currentLocation = World.MARS;

	private WorldState[] worldStates = WorldState[3];

	private float lastSwitched;
	[SerializeField] private float timePerWorld = 30;

	// Use this for initialization
	void Start () {
		lastSwitched = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentLocation) {
			case World.MARS :

				break;
			case World.EARTH :

				break;
			case World.ISS :

				break;
		}

		if (Time.time - lastSwitched > timePerWorld) {
			lastSwitched = Time.time;
			worldStates[currentLocation].playerTransform = player.transform;
			worldStates[currentLocation].playerHealth = player.GetComponent<VRplayerController>().health;
		}
	}
}
