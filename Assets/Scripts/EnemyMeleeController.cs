using UnityEngine;
using System;

[Serializable]
public class EnemyMeleeController : HumanoidController {

	public IMeleeController attackController;
	public bool attacking = false;
	private float attackCoolDownTime = 0;
	
	public void SetAttackController (IMeleeController controller) {
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
		if (IsAttackReady()) {
			attackController.Attack();
			attackCoolDownTime = CalcAttackCoolDown(coolDownPeriodInSeconds);
		} 
	}

	// Virtual so it's mockable for tests
	virtual public bool IsAttackReady() {
		return attackCoolDownTime <= Time.time;
	}

	public float CalcAttackCoolDown(float cdTime) {
		return Time.time + cdTime;
	}
}
