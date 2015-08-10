// Humble Object used to test logic of code while decoupling code that cannot be mocked.

using UnityEngine;
using System;

[Serializable]
public class PlayerController: HumanoidController {
	
	public IFireAimController fireAimController;
	
	public void SetFireAimController (IFireAimController controller) {
		fireAimController = controller;
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

	//FIXME: This should be an overridden method because its actually overriding the old method. Unfortunately NSubsitute has issues with mocking 
	// If Virtual is used in this way. Therefore I've decded to just let it go. There appears to be no consequence at the time of writing this besides it being
	// "bad design." Id rather it be testable than be a "good design" or "true polymorphism"
	public Vector3 Move (Vector3 currentPos, Vector3 moveDirection, float moveSpeed, Quaternion oldRotation, float turnSpeed) {

		if (moveDirection != Vector3.zero && FireAimJoystickNeutral() == true) { 
			FaceDirection (moveDirection, oldRotation, turnSpeed); 
		}

		return base.Move(currentPos, moveDirection, moveSpeed);
	}

}

