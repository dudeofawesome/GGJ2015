using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	[SerializeField] public GameObject player;
	[SerializeField] private bool smart;
	[SerializeField] private AudioSource soundHit;
	[SerializeField] private AudioSource soundDie;
	[SerializeField] private AudioSource soundTalk;
	[SerializeField] private GameObject laserEmitter;

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
		if (!GameEngine.marsPaused) {
			if (smart) move ();
			else move2 ();
		}
	}


	public void bulletHit ( int dmgTaken, int pushbackFactor, bool knockBack = false ) {
		health -= dmgTaken;
		//AudioSource.PlayClipAtPoint
		soundHit.pitch = Random.value * 0.5f + 0.75f;
		soundHit.Play();
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
		soundDie.pitch = Random.value * 2 + 0.5f;
		AudioSource.PlayClipAtPoint(soundDie.clip, transform.position);
		// soundDie.Play();
		Destroy(gameObject);
	}

	// Smart move code, circling the enemy
	public void move () {
		if (Random.Range(0, 200) == 0) {
			soundTalk.pitch = Random.value * 2 + 0.5f;
			soundTalk.Play();
			fireBullet();
		}

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
		Vector3 variation = new Vector3(1,1,1);//new Vector3 (Random.Range (0.8f, 1.2f), Random.Range (0.8f, 1.2f), Random.Range (0.8f, 1.2f));
		if (Physics.Raycast(laserEmitter.transform.position, laserEmitter.transform.forward * 200, out hitInfo, 200f)) {
			if (hitInfo.transform.gameObject.tag == "player") {
				print("ouchhhhh");
				float angle = (float) Mathf.Atan2( (transform.position - hitInfo.rigidbody.transform.position).z, (transform.position - hitInfo.rigidbody.transform.position).x);
				hitInfo.rigidbody.gameObject.GetComponent<VRplayerController>().Hurt( 20, angle);
			}
		}
		print("shooting");
		StartCoroutine(ShowLaser());
	}

	IEnumerator ShowLaser () {
		laserEmitter.GetComponent<LineRenderer>().enabled = true;
		yield return new WaitForSeconds(0.25f);
		laserEmitter.GetComponent<LineRenderer>().enabled = false;
	}
}