// Humble Object used to test logic of code while decoupling code that cannot be mocked.

using UnityEngine;
using System;

[Serializable]
public class PlayerController {
	
	public IFireAimController fireAimController;
	public IMovementController movementController;
	public Vector3 fireDirection;
	public readonly Vector3 initialFireDirection = Vector3.right;
	
	public void SetFireAimController (IFireAimController controller) {
		fireAimController = controller;
	}
	
	public void SetMovementController (IMovementController controller) {
		movementController = controller;
	}
	
	public void ApplyFire() {
		//Makes sure that last fire direction is persisted if a new one isn't entered when stationary firing
		if (!FireAimJoystickNeutral()) { 
			// FIXME: Turns choppy needs to be made smooth.
			movementController.FaceDirection (new Vector3(GetFireAimH(), GetFireAimV())); 
		} 
		
		// Convert to unit vector so projectiles are all going the same speed.
		fireDirection.Normalize(); 
		fireAimController.Fire();
	}
	
	public bool FireAimJoystickNeutral() { return GetFireAimH() == 0 && GetFireAimV() == 0; }
	
	public float GetMoveH() {
		return movementController.GetMoveH(); 
	}
	
	public float GetMoveV() {
		return movementController.GetMoveV(); 
	}
	
	public float GetFireAimH() {
		return fireAimController.GetFireAimH(); 
	}
	
	public float GetFireAimV() {
		return fireAimController.GetFireAimV(); 
	}
	
	public Vector3 CalculateVelocity(float speed) {
		return  new Vector3(fireDirection.x * speed , fireDirection.y * speed,0); 
	}
	
	public Vector3 Move (Vector3 currentPosition, float hAxis, float vAxis, float moveSpeed) {
		Vector3 moveDirection = new Vector2(hAxis,  vAxis);
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		
		if (moveDirection != Vector3.zero && FireAimJoystickNeutral() == true) { 
			movementController.FaceDirection (moveDirection); 
		}
		
		// New position
		return Vector3.Lerp (currentPosition, target, Time.deltaTime); 
	}
	
	public Quaternion FaceDirection(Vector3 newDirection, Quaternion oldOrientation, float turnSpeed) { 
		
		fireDirection = newDirection;
		
		//  Find the angle needed to turn to face new direction player is moving.
		float targetAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
		
		// Rotate player in new direction
		return Quaternion.Slerp(oldOrientation, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );
	}
}

