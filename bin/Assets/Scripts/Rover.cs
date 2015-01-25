using UnityEngine;
using System.Collections;
public class Rover : MonoBehaviour {
	[SerializeField] private GameObject humanBase;

	[SerializeField] float speed = 0.01f,
		health = 100;

	enum States {ACTIVE, INACTIVE};
	States state = States.INACTIVE;

	public void Update() {
		Vector3 distanceFromBase = humanBase.transform.position - transform.position;
		if (state == States.ACTIVE && distanceFromBase.magnitude > 5) { //Second condition is for when rover reaches base
			Vector3 homeVelocityVector = speed * distanceFromBase.normalized;
			transform.position += homeVelocityVector * Time.deltaTime;
			transform.LookAt(humanBase.transform);
		}
	}

	public void damaged ( int damage ) {
		health -= damage;

		if ( health <= 0 ) {
//			gameOver();
		}
	}

	public void turnOn() {
		state = States.ACTIVE;
	} 

	public void turnOff() {
		state = States.INACTIVE;
	}
}