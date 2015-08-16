using UnityEngine;
using System;

[Serializable]
public class EnemyController : HumanoidController {

	public IAttackComponent attackComponent;
	public IMeleeComponent meleeComponent;
	public IRangedComponent rangedAttackComponent;
	public bool attacking = false;

	protected float attackCoolDownTime = 0;
	
	public void SetAttackComponent (IMeleeComponent component) {
		meleeComponent = component;
	}

	public void SetAttackComponent (IRangedComponent component) {
		rangedAttackComponent = component;
	}

	public void SetAttackComponent (IAttackComponent component) {
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

	public void AttemptHit(float coolDownPeriodInSeconds) {
		if (IsAttackReady()) {
			if (rangedAttackComponent == null) {
				meleeComponent.Attack();
			} else {
				rangedAttackComponent.Attack(Vector3.right);
			}
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
