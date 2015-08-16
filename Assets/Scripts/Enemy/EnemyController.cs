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


	public Vector3 MoveAsRanged (Vector3 currentPos, Vector3 target, float moveSpeed) {

		int minDistFromPlayer = 5;
		int maxAttackDist = 10;

		if (!DistanceGreater (maxAttackDist, currentPos, target)) {
			if (attacking == false) {
				attacking = true;
				attackComponent.StartAttacking();
			} 
		} else if (DistanceGreater(maxAttackDist + 1, currentPos, target)) {
			if (attacking == true) {
				attacking = false;
				attackComponent.StopAttacking();
			}
		}
		
		if (!DistanceGreater(minDistFromPlayer, currentPos, target)) {
			return currentPos;
		}
		
		return base.Move(currentPos, target, moveSpeed); 
	}
	
	public new Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed) {

		if (DistanceGreater(1,currentPos, target)) {
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

	private bool DistanceGreater(int units, Vector3 currentPos, Vector3 target) {
		return Vector3.Distance(currentPos, target) > units;
	}

	public void AttemptHit(float coolDownPeriodInSeconds) {
		if (IsAttackReady()) {
			if (rangedAttackComponent == null) {
				meleeComponent.Attack();
			} else {
				rangedAttackComponent.Attack(Aim());
			}
			attackCoolDownTime = CalcAttackCoolDown(coolDownPeriodInSeconds);
		} 
	}

	public Vector3 Aim() {
		return rangedAttackComponent.Aim();
	}

	// Virtual so it's mockable for tests
	virtual public bool IsAttackReady() {
		return attackCoolDownTime <= Time.time;
	}

	public float CalcAttackCoolDown(float cdTime) {
		return Time.time + cdTime;
	}
}
