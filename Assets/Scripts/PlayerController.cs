// Humble Object used to test logic of code while decoupling code that cannot be mocked.

using UnityEngine;
using System;

[Serializable]
public class PlayerController {
	
	public IFiringController firingController;
	public IMovingController movingController;
	public bool firing = false;
	public Vector3 fireDirection;
	
	public readonly Vector3 initialFireDirection = Vector3.right;
	
	public void SetFiringController (IFiringController controller) {
		firingController = controller;
	}
	
	public void SetMovingController (IMovingController controller) {
		movingController = controller;
	}
	
	public void ApplyFire() {
		
		// Second condition: Makes sure that last fire direction is persisted if a new one isn't entered when stationary firing
		if (firing == true && GetFireAimH() != 0 && GetFireAimV() != 0) { // TODO: Check conditions
			// FIXME: Turns choppy needs to be made smooth.
			movingController.FaceDirection (new Vector3(GetFireAimH(), GetFireAimV())); // TODO: Test function call
		} 
		
		// Convert to unit vector so projectiles are all going the same speed.
		fireDirection.Normalize(); // TODO: Test normalized
		firingController.Fire();
	}
	
	public void IsFiring() { 
		if (firing == true) {
			firing = false;
		}
		firing = true;
	}
	
	public float GetMoveH() {
		return movingController.GetMoveH(); // TODO: Mock and test
	}
	
	public float GetMoveV() {
		return movingController.GetMoveV(); // TODO: Mock and test
	}
	
	public float GetFireAimH() {
		return firingController.GetFireAimH(); // TODO: Mock and test
	}
	
	public float GetFireAimV() {
		return firingController.GetFireAimV(); // TODO: Mock and test
	}
	
	public Vector3 CalculateVelocity(float speed) {
		return  new Vector3(fireDirection.x * speed , fireDirection.y * speed,0); // TODO: Test velocity set correctly
	}
	
	public Vector3 Move (Vector3 currentPosition, float hAxis, float vAxis, float moveSpeed) {
		Vector3 moveDirection = new Vector2(hAxis,  vAxis);
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		
		if (moveDirection != Vector3.zero && firing != true) { // TODO: Test conditions
			movingController.FaceDirection (moveDirection); // TODO: Test called AND function gives right output
		}
		
		// New position
		return Vector3.Lerp (currentPosition, target, Time.deltaTime); // TODO: Test move position is now correct
	}
	
	public Quaternion FaceDirection(Vector3 newDirection, Quaternion oldOrientation, float turnSpeed) { // TODO: Test input and output
		
		fireDirection = newDirection;
		
		//  Find the angle needed to turn to face new direction player is moving.
		float targetAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
		
		// Rotate player in new direction
		return Quaternion.Slerp(oldOrientation, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );
	}
}

