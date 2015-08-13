using UnityEngine;
using System;

[Serializable]
public class EnemyController : HumanoidController {

	public IAttackController attackController;
	public bool attacking = false;
	private float timeStamp = 0;
	
	public void SetAttackController (IAttackController controller) {
		attackController = controller;
	}

	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed, Quaternion oldRotation, float turnSpeed) {
		if (Vector3.Distance(currentPos, target) > 1) {
			if (attacking == true) {
				attacking = false;
				attackController.StopAttacking();
			}
			return base.Move(currentPos, target, moveSpeed); 
		}

		if (attacking == false) {
			attacking = true;
			attackController.StartAttacking();
		} 

		return currentPos;
	}

	public void AttemptHit(float coolDownPeriodInSeconds) {
		if (isAttackReady()) {
			attackController.Attack();
			timeStamp = Time.time + coolDownPeriodInSeconds;
		} 
	}

	public bool isAttackReady() {
		return timeStamp <= Time.time;
	}
}
