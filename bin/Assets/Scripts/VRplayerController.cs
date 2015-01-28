using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class VRplayerController : MonoBehaviour {

	[SerializeField] private GameObject leftEye;
	[SerializeField] private GameObject rightEye;
	[SerializeField] private GameObject cardboardMain;
	[SerializeField] private GameObject cardboardHead;
	[SerializeField] private GameObject hands;
	[SerializeField] private GameObject arms;
	[SerializeField] private GameObject laserEmitter;
	[SerializeField] private Light gunFlare;
	[SerializeField] private AudioSource shootSound;
	[SerializeField] private Menu2 GUI;

	private Vector3 tmpV3 = new Vector3();
	[SerializeField] public int health = 100;

	private float MAXSPEED = 3;


	// Use this for initialization
	void Start () {
		HOTween.Init(false, false, true);
		HOTween.EnableOverwriteManager();
	}
	
	// Update is called once per frame
	void Update () {
		tmpV3.Set (Input.GetAxis("Strafe"), 0, -Input.GetAxis("Forward"));
		float mag = tmpV3.magnitude;
		float angle = Mathf.Atan2(tmpV3.z, tmpV3.x);
		float angle2 = cardboardHead.transform.rotation.eulerAngles.y * Mathf.PI / 180;
		tmpV3.Set (Mathf.Cos (angle - angle2), 0, Mathf.Sin (angle - angle2));
		tmpV3 *= mag;
		rigidbody.AddRelativeForce(tmpV3 * 10000, ForceMode.Force);
		rigidbody.velocity = (rigidbody.velocity.magnitude > MAXSPEED) ? rigidbody.velocity.normalized * MAXSPEED : rigidbody.velocity;
		// Move hands for fine tuning aim
		hands.transform.localRotation = Quaternion.Euler(-Input.GetAxis("AimUpDown") * 15, Input.GetAxis("AimSide") * 15 + 180, 0);
		// Jump
		if (Input.GetButtonDown("Jump")) {
			rigidbody.AddRelativeForce(0, 300000, 0);
		}
		if (Input.GetAxis("Ironsights") > 0 || Input.GetMouseButton(1)) {
			print("ironing");
			arms.transform.position.Set(-0.04f, -0.2f, 0f);
		} else {
			print("not ironing");
			arms.transform.position.Set(0, -0.294f, 0f);
		}
		if (Input.GetAxis("FireLaser") > 0 || Input.GetMouseButtonDown(0)) {
			StartCoroutine(ShowLaser());
			Shoot();
		}
	}

	void Shoot () {
		HOTween.To(gunFlare, 0f, "intensity", 1);
		gunFlare.enabled = true;
		gunFlare.intensity = 1;
		HOTween.To(gunFlare, 0.5f, "intensity", 0);
		shootSound.Play();
		RaycastHit hitInfo = new RaycastHit ();
		if (Physics.Raycast(laserEmitter.transform.position, laserEmitter.transform.forward * 200, out hitInfo, 200f)) {
			if (hitInfo.transform.gameObject.tag == "enemy") {
				hitInfo.transform.gameObject.GetComponent<Enemy>().bulletHit(50, 100, true);
			}
		}
	}

	// TODO ADD RED CIRCLE THING
	public void Hurt (int dmgTaken, float angle) {
		print("player hit");

		health -= dmgTaken;
		GUI.StartDirectionHit(0.5f, angle);

		if (health <= 0) {
			//gameOver();		
		}
	}

	IEnumerator ShowLaser () {
		laserEmitter.GetComponent<LineRenderer>().enabled = true;
		yield return new WaitForSeconds(0.25f);
		laserEmitter.GetComponent<LineRenderer>().enabled = false;
	}

}
