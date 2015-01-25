<<<<<<< HEAD
﻿using UnityEngine;
=======
﻿/*using UnityEngine;
>>>>>>> mathew_cha
using System.Collections;

public class GameEngine : MonoBehaviour {

	[SerializeField] private GameObject player;

<<<<<<< HEAD
	enum World {MARS, EARTH, ISS};
	[SerializeField] private World currentLocation = World.MARS;
	private bool changedWorldThisFrame = true;

	private WorldState[] worldStates = new WorldState[3];
	[SerializeField] private Material skyboxMars;
	[SerializeField] private Material skyboxISS;
	[SerializeField] private Material skyboxEarth;
	public static bool marsPaused = true;
	public static bool earthPaused = true;
	public static bool ISSpaused = true;

	private float lastSwitched;
	[SerializeField] private float TIMEPERWORLD = 10;
=======
	enum World {EARTH, MARS, ISS};
	[SerializeField] private World currentLocation = World.MARS;
	private bool changedWorldThisFrame = true;

	private WorldState[] worldStates = WorldState[3];

	private float lastSwitched;
	[SerializeField] private float TIMEPERWORLD = 30;
>>>>>>> mathew_cha
	private readonly float WORLDCHANGEHINTTIME = 5;

	// Use this for initialization
	void Start () {
		lastSwitched = Time.time;
<<<<<<< HEAD
		worldStates[(int) World.MARS] = new WorldState();
		worldStates[(int) World.EARTH] = new WorldState();
		worldStates[(int) World.ISS] = new WorldState();
		worldStates[(int) World.MARS].gravity = 3.711f;
		worldStates[(int) World.EARTH].gravity = 9.81f;
		worldStates[(int) World.ISS].gravity = 0;
		worldStates[(int) World.MARS].skybox = skyboxMars;
		worldStates[(int) World.EARTH].skybox = skyboxEarth;
		worldStates[(int) World.ISS].skybox = skyboxISS;
		worldStates[(int) World.MARS].spawnPoint = new Vector3(0, 6.34f, 0);
		worldStates[(int) World.EARTH].spawnPoint = new Vector3(100, 0, 0);
		worldStates[(int) World.ISS].spawnPoint = new Vector3(-100, 0, 0);
		worldStates[(int) World.MARS].spawnRotation = new Vector3(0, 0, 0);
		worldStates[(int) World.EARTH].spawnRotation = new Vector3(0, 0, 0);
		worldStates[(int) World.ISS].spawnRotation = new Vector3(0, 0, 0);
=======
>>>>>>> mathew_cha
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
=======
		if (changedWorldThisFrame) {
			if (worldStates[currentLocation].firstVisit) {
				// World Setup

				worldStates[currentLocation].firstVisit = false;
			} else {
				// Restart all AI etc for the world
			}

			changedWorldThisFrame = false;
		}

>>>>>>> mathew_cha
		switch (currentLocation) {
			case World.MARS :

				break;
			case World.EARTH :

				break;
			case World.ISS :

				break;
		}

<<<<<<< HEAD
		if (Time.time - lastSwitched > TIMEPERWORLD - WORLDCHANGEHINTTIME) {
			// Play world switch warning
		}
		if (Time.time - lastSwitched > TIMEPERWORLD) {
			lastSwitched = Time.time;
			changedWorldThisFrame = true;
			worldStates[(int) currentLocation].playerPosition = player.transform.position;
			worldStates[(int) currentLocation].playerVeclocity = player.rigidbody.velocity;
			worldStates[(int) currentLocation].playerRotation = player.transform.rotation;
			worldStates[(int) currentLocation].playerHealth = player.GetComponent<VRplayerController>().health;
			switch (currentLocation) {
				case World.MARS :
					currentLocation = World.EARTH;
					break;
				case World.EARTH :
					currentLocation = World.ISS;
					break;
				case World.ISS :
					currentLocation = World.MARS;
					break;
			}
			player.transform.position = worldStates[(int) currentLocation].playerPosition;
			player.rigidbody.velocity = worldStates[(int) currentLocation].playerVeclocity;
			player.transform.rotation = worldStates[(int) currentLocation].playerRotation;
			player.transform.Find("Main Camera/Main Camera Left").GetComponent<Skybox>().material = worldStates[(int) currentLocation].skybox;
			player.transform.Find("Main Camera/Main Camera Right").GetComponent<Skybox>().material = worldStates[(int) currentLocation].skybox;
			player.GetComponent<VRplayerController>().health = worldStates[(int) currentLocation].playerHealth;


			if (worldStates[(int) currentLocation].firstVisit) {
				// World Setup
				player.transform.position = worldStates[(int) currentLocation].playerPosition;
				player.transform.rotation = worldStates[(int) currentLocation].playerRotation;
				worldStates[(int) currentLocation].firstVisit = false;
			} else {
				// Restart all AI etc for the world
			}
		}
	}
}
=======
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
>>>>>>> mathew_cha
