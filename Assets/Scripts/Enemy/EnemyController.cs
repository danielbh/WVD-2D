using UnityEngine;
using System;

[Serializable]
public class EnemyController : HumanoidController {

	public IMeleeComponent attackComponent;
	public bool attacking = false;

	protected float attackCoolDownTime = 0;
	
	public void SetAttackComponent (IMeleeComponent component) {
		attackComponent = component;
	}


	public new Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed/*, Quaternion oldRotation, float turnSpeed*/) {
		if (Vector3.Distance(currentPos, target) > 1) {
			if (attacking == true) {
				attacking = false;
				attackComponent.StopAttacking();			
			}
			return base.Move(currentPos, target, moveSpeed); 
		}

		if (attacking == false) {
			attacking = true;
			attackComponent.StartAttacking();

		} 
		return currentPos;
	}

	// FIXME: Lose test coverage if you do this day 17
//	virtual public void StartAttacking() {
//
//	}
//
//	virtual public void StopAttacking() {
//	}

	public void AttemptHit(float coolDownPeriodInSeconds) {
		if (IsAttackReady()) {
			attackComponent.Attack();
			attackCoolDownTime = CalcAttackCoolDown(coolDownPeriodInSeconds);
		} 
	}

	// FIXME: Lose test coverage if you do this
//	virtual public void Attack() {
//	}

	// Virtual so it's mockable for tests
	virtual public bool IsAttackReady() {
		return attackCoolDownTime <= Time.time;
	}

	public float CalcAttackCoolDown(float cdTime) {
		return Time.time + cdTime;
	}
}
