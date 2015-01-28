using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject cameraLeft;
	[SerializeField] private GameObject cameraRight;

	enum World {MARS, EARTH, ISS};
	[SerializeField] private World currentLocation = World.MARS;
	private bool changedWorldThisFrame = true;

	private WorldState[] worldStates = new WorldState[3];
	[SerializeField] private Material skyboxMars;
	[SerializeField] private Material skyboxISS;
	[SerializeField] private Material skyboxEarth;
	[SerializeField] private AudioSource musicMars;
	[SerializeField] private AudioSource musicISS;
	[SerializeField] private AudioSource musicEarth;
	public static bool marsPaused = false;
	public static bool earthPaused = true;
	public static bool ISSpaused = true;

	private float lastSwitched;
	[SerializeField] private float TIMEPERWORLD = 10;
	private readonly float WORLDCHANGEHINTTIME = 5;

	[SerializeField] private GameObject marsRover;
	[SerializeField] private SphereCollider marsRoverAlienSpawn;
	private int MARS_ROVER_INTERACTION_BOUND = 4;
	[SerializeField] private GameObject flyingAlien;
	[SerializeField] private GameObject dumbAlien;

	// Use this for initialization
	void Start () {
		lastSwitched = Time.time;
		worldStates[(int) World.MARS] = new WorldState();
		worldStates[(int) World.EARTH] = new WorldState();
		worldStates[(int) World.ISS] = new WorldState();
		worldStates[(int) World.MARS].gravity = 3.711f;
		worldStates[(int) World.EARTH].gravity = 9.81f;
		worldStates[(int) World.ISS].gravity = 9.81f;
		worldStates[(int) World.MARS].skybox = skyboxMars;
		worldStates[(int) World.EARTH].skybox = skyboxEarth;
		worldStates[(int) World.ISS].skybox = skyboxISS;
		worldStates[(int) World.MARS].music = musicMars;
		worldStates[(int) World.EARTH].music = musicEarth;
		worldStates[(int) World.ISS].music = musicISS;
		worldStates[(int) World.MARS].spawnPoint = new Vector3(0, 6.34f, 0);
		worldStates[(int) World.EARTH].spawnPoint = new Vector3(4.347498f, -73.0188f, 3570.857f);
		worldStates[(int) World.ISS].spawnPoint = new Vector3(10.35f, -35.91f, -3570);
		worldStates[(int) World.MARS].spawnRotation = new Vector3(0, 0, 0);
		worldStates[(int) World.EARTH].spawnRotation = new Vector3(0, 243.7267f, 0);
		worldStates[(int) World.ISS].spawnRotation = new Vector3(0, 90, 0);
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentLocation) {
			case World.MARS :
				if ((marsRover.transform.position - player.transform.position).magnitude < MARS_ROVER_INTERACTION_BOUND) {
					marsRover.gameObject.GetComponent<Rover>().turnOn();
					// Spawn me ma aliens
					if (Random.Range(0, 100) == 0) {
						Vector3 spawnPoint;
						switch (Random.Range(0, 2)) {
							case 0:
								spawnPoint = new Vector3(-6.08f, 5.54f, 79.36f);
								break;
							case 1:
								spawnPoint = new Vector3(-60.01f, 9.87f, -130.71f);
								break;
							default:
								spawnPoint = new Vector3(-60.01f, 9.87f, -130.71f);
								break;
						}
						GameObject enemy = (GameObject) Instantiate(flyingAlien, spawnPoint, Quaternion.Euler(0, 0, 0));
						enemy.GetComponent<Enemy>().player = player;
					}
					if (Random.Range(0, 100) == 0) {
						Vector3 spawnPoint;
						switch (Random.Range(0, 2)) {
							case 0:
								spawnPoint = new Vector3(-6.08f, 5.54f, 79.36f);
								break;
							case 1:
								spawnPoint = new Vector3(-60.01f, 9.87f, -130.71f);
								break;
							default:
								spawnPoint = new Vector3(-60.01f, 9.87f, -130.71f);
								break;
						}
						GameObject enemy = (GameObject) Instantiate(dumbAlien, spawnPoint, Quaternion.Euler(0, 0, 0));
						enemy.GetComponent<Enemy>().player = player;
					}
				}
				break;
			case World.EARTH :

				break;
			case World.ISS :

				break;
		}

		if (Time.time - lastSwitched > TIMEPERWORLD - WORLDCHANGEHINTTIME) {
			// Play world switch warning
		}
		if (Time.time - lastSwitched > TIMEPERWORLD) {
			lastSwitched = Time.time;
			changedWorldThisFrame = true;
			worldStates[(int) currentLocation].playerPosition = player.transform.position;
			worldStates[(int) currentLocation].playerVeclocity = player.rigidbody.velocity;
			worldStates[(int) currentLocation].playerRotation = player.transform.rotation;
			worldStates[0].music.Stop();
			worldStates[1].music.Stop();
			worldStates[2].music.Stop();
			worldStates[(int) currentLocation].playerHealth = player.GetComponent<VRplayerController>().health;
			switch (currentLocation) {
				case World.MARS :
					currentLocation = World.EARTH;
					marsPaused = true;
					earthPaused = false;
					ISSpaused = true;
					worldStates[(int) currentLocation].music.Play();
					break;
				case World.EARTH :
					currentLocation = World.ISS;
					marsPaused = true;
					earthPaused = true;
					ISSpaused = false;
					worldStates[(int) currentLocation].music.Play();
					break;
				case World.ISS :
					currentLocation = World.MARS;
					marsPaused = false;
					earthPaused = true;
					ISSpaused = true;
					worldStates[(int) currentLocation].music.Play();
					break;
			}
			player.transform.position = worldStates[(int) currentLocation].playerPosition;
			player.rigidbody.velocity = worldStates[(int) currentLocation].playerVeclocity;
			player.transform.rotation = worldStates[(int) currentLocation].playerRotation;
			cameraLeft.GetComponent<Skybox>().material = worldStates[(int) currentLocation].skybox;
			cameraRight.GetComponent<Skybox>().material = worldStates[(int) currentLocation].skybox;
			player.GetComponent<VRplayerController>().health = worldStates[(int) currentLocation].playerHealth;


			if (worldStates[(int) currentLocation].firstVisit) {
				// World Setup
				player.transform.position = worldStates[(int) currentLocation].spawnPoint;
				player.transform.rotation = Quaternion.Euler(worldStates[(int) currentLocation].spawnRotation.x, worldStates[(int) currentLocation].spawnRotation.y, worldStates[(int) currentLocation].spawnRotation.z);
				worldStates[(int) currentLocation].firstVisit = false;
			} else {
				// Restart all AI etc for the world
			}
		}
	}
}