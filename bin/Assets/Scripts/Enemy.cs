using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	[SerializeField] private GameObject player;
	[SerializeField] private bool smart;

	private static int ENEMY_DISTANCE_RANGE = 30,
					   ENEMY_BUFFER = 10; // How far/close the enemies are allowed to get
	private int health = 100;
	private float linearSpeed = 15f, angularVelocity = .005f, 
				idealDistanceFromPlayer, startTime;

	public void Start () {
		startTime = Time.time;
		idealDistanceFromPlayer = Random.value * ENEMY_DISTANCE_RANGE + ENEMY_BUFFER;
		angularVelocity += Random.value * 0.005f;
		angularVelocity *= ( Mathf.Floor( Random.value * 2 ) * 2 ) - 1; // THIS IS IN RADIANS. The tail code just makes a random -1 or 1.
	}

	public void Update () {
<<<<<<< HEAD
		if (!GameEngine.marsPaused) {
			if (smart) move ();
			else move2 ();
		}
=======
		if (smart)
			move ();
		else
			move2 ();
>>>>>>> mathew_cha
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
			transform.position = new Vector3(Mathf.Cos(angle + angularVelocity) * distanceFromEnemy.magnitude + player.transform.position.x, transform.position.y, Mathf.Sin(angle + angularVelocity) * distanceFromEnemy.magnitude + player.transform.position.z);
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
		RaycastHit hitInfo = new RaycastHit ();
		Vector3 variation = new Vector3 (Random.Range (0.8f, 2f), Random.Range (0.8f, 2f), Random.Range (0.8f, 2f));
		if (Physics.Raycast(transform.position, 
		                    new Vector3(transform.forward.x * variation.x, transform.forward.y * variation.y, transform.forward.z * variation.z),
		    				out hitInfo, 200f)) {
			if (hitInfo.rigidbody.gameObject.tag == "player") {
				double angle = Mathf.Atan2( (transform.position - hitInfo.rigidbody.transform.position).z, (transform.position - hitInfo.rigidbody.transform.position).x);
				hitInfo.rigidbody.gameObject.GetComponent<VRplayerController>().Hurt( 20, angle); 
			}
		}
	}
}