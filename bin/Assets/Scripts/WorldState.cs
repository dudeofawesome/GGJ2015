using UnityEngine;
using System.Collections;

public class WorldState {
	public Vector3 playerPosition;
	public Vector3 playerVeclocity;
	public Vector3 playerAcceleration;
	public Quaternion playerRotation;
	public int playerHealth;

	public bool firstVisit = true;

	public float gravity = 9.81f;
	public Vector3 spawnPoint;
	public Vector3 spawnRotation;
	public Material skybox;

	public WorldState () {

	}
}
