using UnityEngine;
using System.Collections;
<<<<<<< HEAD

public class Rover : MonoBehaviour {
	[SerializeField] private GameObject humanBase;

=======

public class Rover : MonoBehaviour {

	[SerializeField] private GameObject humanBase;
>>>>>>> mathew_cha
	int speed = 6,
		health = 100;

	enum States {ACTIVE, INACTIVE};
	States state = States.INACTIVE;

	public void Update() {
		if (state == States.ACTIVE) {
			Vector3 homeVelocityVector = speed * (humanBase.transform.position - transform.position).normalized;
			transform.position += homeVelocityVector;
			transform.LookAt(humanBase.transform);
		}
	}

	public void damaged ( int damage ) {
		health -= damage;

		if ( health <= 0 ) {
<<<<<<< HEAD
//			gameOver();
=======
			//gameOver();
>>>>>>> mathew_cha
		}

	}

	public void switchState() {
		if ( state == States.ACTIVE ) {
			state = States.INACTIVE;
		} else {
			state = States.ACTIVE;
		}
	}
}