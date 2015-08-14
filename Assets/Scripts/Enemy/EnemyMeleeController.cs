using UnityEngine;
using System;

[Serializable]
public class EnemyMeleeController : HumanoidController {

	public IMeleeController attackController;
	new public IDestroyableController hpController;
	public bool attacking = false;
	private float attackCoolDownTime = 0;
	
	public void SetAttackController (IMeleeController controller) {
		attackController = controller;
	}

	public void SetHitPointsController (IDestroyableController controller) {
		hpController = controller;
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

	public new void ReduceHitPoints (int damage, int currentHP) {
		if (damage >= currentHP) {
			// Game over for player
			hpController.Destroy ();
		} else {
			hpController.ReduceHitPoints(damage);
		}
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
