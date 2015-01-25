using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	[SerializeField] private GameObject player;

	private static int ENEMY_DISTANCE_RANGE = 100, 
					   ENEMY_BUFFER = 30; // How far/close the enemies are allowed to get

	private int health = 100, velocity = 5;
	private float linearSpeed = 10f, angularVelocity = .001f, 
				idealDistanceFromPlayer, startTime;

	public void Start () {
		startTime = Time.time;
		idealDistanceFromPlayer = Random.value * ENEMY_DISTANCE_RANGE + ENEMY_BUFFER;
		angularVelocity += Random.value * 0.005f;
		//angularVelocity *= ( Mathf.Floor( Random.value * 2 ) * 2 ) - 1; // THIS IS IN RADIANS. The tail code just makes a random -1 or 1.
	}

	public void Update () {
		move();
	}


	public void bulletHit ( int dmgTaken, int pushbackFactor, bool knockBack = false ) {
		
		health -= dmgTaken;
		//AudioSource.PlayClipAtPoint

		if ( health <= 0 ) {
			die();
			return;
		}

		if (knockBack) {
			// Push back code
			Vector3 distanceFromEnemy = transform.position - player.transform.position;
			rigidbody.AddForce( pushbackFactor * distanceFromEnemy.normalized );
		}
	}

	private void die () {

	}

	// Smart move code, circling the enemy
	public void move () {
		Vector3 distanceFromEnemy = player.transform.position - transform.position;

		// If not in ideal range, beeline, if so, circle
		if ( distanceFromEnemy.magnitude > idealDistanceFromPlayer && distanceFromEnemy.magnitude > idealDistanceFromPlayer - 0.5f ) {
			transform.position += linearSpeed * distanceFromEnemy.normalized * Time.deltaTime;
		} else {
			float angle = Mathf.Atan2(player.transform.position.z - transform.position.z, player.transform.position.x - transform.position.x) + Mathf.PI;
			transform.position = new Vector3(Mathf.Cos(angle + angularVelocity) * distanceFromEnemy.magnitude, transform.position.y, Mathf.Sin(angle + angularVelocity) * distanceFromEnemy.magnitude);
		}
		transform.LookAt (player.transform);
	}

	// Dumb move
	public void move2 () {
		Vector3 distanceFromEnemy = player.transform.position - transform.position;
		transform.position += linearSpeed * distanceFromEnemy.normalized * Time.deltaTime;
		transform.LookAt (player.transform);
	}

	public void fireBullet () {

	}
}