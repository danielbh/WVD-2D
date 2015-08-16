using UnityEngine;
using System;

[Serializable]
public class HumanoidController {

	public IMoveComponent moveComponent;
	public Vector3 currentFireDirection;

	public void SetMoveComponent (IMoveComponent component) {
		moveComponent = component;
	}

	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed) {
		return moveComponent.Move (currentPos, target, moveSpeed * Time.deltaTime); 
	}
	
	public void FaceDirection(Vector3 newDirection, Quaternion oldRotation, float turnSpeed) { 
		
		newDirection.Normalize();
		currentFireDirection = newDirection;
		
		//  Find the angle needed to turn to face new direction player is moving.
		float targetAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
		
		// Rotate player in new direction
		var newRotation = Quaternion.Slerp(oldRotation, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );
		
		moveComponent.FaceDirection(newRotation);
	}
}
