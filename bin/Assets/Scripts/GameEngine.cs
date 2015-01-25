/*using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	[SerializeField] private GameObject player;

	enum World {EARTH, MARS, ISS};
	[SerializeField] private World currentLocation = World.MARS;
	private bool changedWorldThisFrame = true;

	private WorldState[] worldStates = WorldState[3];

	private float lastSwitched;
	[SerializeField] private float TIMEPERWORLD = 30;
	private readonly float WORLDCHANGEHINTTIME = 5;

	// Use this for initialization
	void Start () {
		lastSwitched = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (changedWorldThisFrame) {
			if (worldStates[currentLocation].firstVisit) {
				// World Setup

				worldStates[currentLocation].firstVisit = false;
			} else {
				// Restart all AI etc for the world
			}

			changedWorldThisFrame = false;
		}

		switch (currentLocation) {
			case World.MARS :

				break;
			case World.EARTH :

				break;
			case World.ISS :

				break;
		}

		if (Time.time - lastSwitched > TIMEPERWORLD - WORLDCHANGEHINTTIME)
		if (Time.time - lastSwitched > TIMEPERWORLD) {
			lastSwitched = Time.time;
			changedWorldThisFrame = true;
			worldStates[currentLocation].playerTransform = player.transform;
			worldStates[currentLocation].playerHealth = player.GetComponent<VRplayerController>().health;
			currentLocation = currentLocation.next;
		}
	}
}
*/