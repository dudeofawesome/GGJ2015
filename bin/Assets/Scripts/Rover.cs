Using UnityEngine;
Using System.Collection;

public class Rover {
	int speed = 6,
		health = 100;

	Enum States = {ACTIVE, INACTIVE};
	States state = State.INACTIVE;

	private void move() {
		if (state == States.ACTIVE) {
			Vector3 homeVelocityVector = speed * (humanBase.transform.position - transform.position).normalized;
			transform.position += homeVelocityVector;
		}
	}

	public void damaged ( int damage ) {
		health -= damage;

		if ( health <= 0 ) {
			gameOver();
		}

	}

	public void switchState() {
		if ( state == States.ACTIVE ) {
			state = States.INACTIVE;
		} else {
			state = States.ACTIVE
		}
	}
}