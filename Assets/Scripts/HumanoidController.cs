using UnityEngine;
using System;

[Serializable]
public class HumanoidController {

	public IMoveController moveController;
	public IHitPointsController hpController;
	public Vector3 currentFireDirection;

	public void SetMoveController (IMoveController controller) {
		moveController = controller;
	}

	public void SetHitPointsController (IHitPointsController controller) {
		hpController = controller;
	}
	
	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed) {
		return moveController.Move (currentPos, target, moveSpeed * Time.deltaTime); 
	}
	
	public void FaceDirection(Vector3 newDirection, Quaternion oldRotation, float turnSpeed) { 
		
		newDirection.Normalize();
		currentFireDirection = newDirection;
		
		//  Find the angle needed to turn to face new direction player is moving.
		float targetAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
		
		// Rotate player in new direction
		var newRotation = Quaternion.Slerp(oldRotation, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );
		
		moveController.FaceDirection(newRotation);
	}

	public void ReduceHitPoints (int damage, int currentHP) {
		if (damage >= currentHP) {
			// TODO: Game over for player
		} else {
			hpController.ReduceHitPoints(damage);
		}
	}
}
