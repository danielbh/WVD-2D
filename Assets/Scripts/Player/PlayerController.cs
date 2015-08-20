// Humble Object used to test logic of code while decoupling code that cannot be mocked.

using UnityEngine;
using System;

[Serializable]
public class PlayerController: HumanoidController {
	
	public IRangedComponent attackComponent;
	
	public void SetAttackComponent (IRangedComponent component) {
		attackComponent = component;
	}
	
	public void ApplyFire(Quaternion oldFireDirection, float turnSpeed) {

		var direction = GetFireAimAxes();

		if (!FireAimJoystickNeutral()) { 
			// FIXME: Turns choppy needs to be made smooth. I feel like some of these arguments not neccesary.
			FaceDirection(direction, oldFireDirection, turnSpeed); 
		// If player shoots without moving when game first starts.
		} else if (currentFireDirection == Vector3.zero) {
			currentFireDirection = Vector3.right;
		}

		// Use class variable if joystick is neutral. This preserves last aimed direction.
		attackComponent.Attack(this.currentFireDirection);
	}
	
	public bool FireAimJoystickNeutral() { return GetFireAimAxes() == Vector3.zero; }

	public Vector3 GetFireAimAxes() { return attackComponent.Aim(); }
	
	public Vector3 Move (Vector3 currentPos, Vector3 moveDirection, float moveSpeed, Quaternion oldRotation, float turnSpeed) {

		Vector3 target = moveDirection * moveSpeed + currentPos;

		if (moveDirection != Vector3.zero && FireAimJoystickNeutral() == true) { 
			FaceDirection (moveDirection, oldRotation, turnSpeed); 
		}

		return base.Move(currentPos, target, moveSpeed);
	}

}

