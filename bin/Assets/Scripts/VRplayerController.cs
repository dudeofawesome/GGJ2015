using UnityEngine;
using System.Collections;

public class VRplayerController : MonoBehaviour {

	[SerializeField] private GameObject leftEye;
	[SerializeField] private GameObject rightEye;
	[SerializeField] private GameObject cardboardMain;
	[SerializeField] private GameObject cardboardHead;
	[SerializeField] private GameObject hands;
	[SerializeField] private GameObject laserEmitter;

	private Vector3 tmpV3 = new Vector3();
	[SerializeField] private int health = 100;


	// Use this for initialization
	void Start () {
	
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
		rigidbody.velocity = (rigidbody.velocity.magnitude > 2) ? rigidbody.velocity.normalized * 2 : rigidbody.velocity;
		hands.transform.localRotation = Quaternion.Euler(-Input.GetAxis("AimUpDown") * 15, Input.GetAxis("AimSide") * 15 + 180, 0);
	}

	void Shoot () {
		RaycastHit hitInfo = new RaycastHit ();
		if (Physics.Raycast(laserEmitter.transform.position, laserEmitter.transform.forward, out hitInfo, 200f)) {
			if (hitInfo.rigidbody.gameObject.tag == "enemy") {
				hitInfo.rigidbody.gameObject.GetComponent<Enemy>().bulletHit(50, 20, true);
			}
		}
	}

	// TODO ADD RED CIRCLE THING
	public void Hurt (int dmgTaken, double angle) {
		health -= dmgTaken;


		if (health <= 0) {
			//gameOver();		
		}
	}

}
