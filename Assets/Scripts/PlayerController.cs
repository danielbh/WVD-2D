// Humble Object used to test logic of code while decoupling code that cannot be mocked.

using UnityEngine;
using System;

[Serializable]
public class PlayerController {
	
	public IFireAimController fireAimController;
	public IMoveController moveController;
	public Vector3 currentFireDirection;
	
	public void SetFireAimController (IFireAimController controller) {
		fireAimController = controller;
	}
	
	public void SetMoveController (IMoveController controller) {
		moveController = controller;
	}
	
	public void ApplyFire(Quaternion oldFireDirection, float turnSpeed) {

		var direction = GetFireAimAxes();

		direction.Normalize();

		if (!FireAimJoystickNeutral()) { 
			// FIXME: Turns choppy needs to be made smooth. I feel like some of these arguments not neccesary.
			FaceDirection(direction, oldFireDirection, turnSpeed); 
		// If player shoots without moving when game first starts.
		} else if (currentFireDirection == Vector3.zero) {
			currentFireDirection = Vector3.right;
		}

		// Use class variable if joystick is neutral. This preserves last aimed direction.
		fireAimController.Fire(this.currentFireDirection);
	}
	
	public bool FireAimJoystickNeutral() { return GetFireAimAxes() == Vector3.zero; }

	public Vector3 GetFireAimAxes() { return fireAimController.GetFireAimAxes(); }
	
	public Vector3 Move (Vector3 currentPos, Vector3 moveAxes, float moveSpeed, Quaternion oldDirection, float turnSpeed) {
		Vector3 moveDirection = moveAxes;
		Vector3 target = moveDirection * moveSpeed + currentPos;
		
		if (moveDirection != Vector3.zero && FireAimJoystickNeutral() == true) { 
			FaceDirection (moveDirection, oldDirection, turnSpeed); 
		}
		
		// New position
		return moveController.VectorLerp (currentPos, target, Time.deltaTime); 
	}
	
	public void FaceDirection(Vector3 newDirection, Quaternion oldDirection, float turnSpeed) { 

		newDirection.Normalize();
		currentFireDirection = newDirection;
		
		//  Find the angle needed to turn to face new direction player is moving.
		float targetAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
		
		// Rotate player in new direction
		var newOrientation = Quaternion.Slerp(oldDirection, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );

		moveController.FaceDirection(newOrientation);
	}
}

