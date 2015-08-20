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
	
	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed, float minDistFromPlayer) {
		if (DistanceGreaterThan(minDistFromPlayer, currentPos, target)) {
			return base.Move(currentPos, target, moveSpeed); 
		}
		return currentPos;
	}

	public void HandleAttacks(Vector3 currentPos, Vector3 target, float maxAttackDist) {

		if (DistanceGreaterThan (maxAttackDist, currentPos, target)) {
			if (attacking == true) {
				attacking = false;
				attackComponent.StopAttacking();
			}
		} else {
			if (attacking == false) {
				attacking = true;
				attackComponent.StartAttacking();
			} 
		}
	}

	// If the distance between currentPos and target are more than units.
	private bool DistanceGreaterThan(float units, Vector3 currentPos, Vector3 target) {
		return Vector3.Distance(currentPos, target) > units;
	}

	// If the distance between currentPos and target are less than units.
	private bool DistanceLessThan(float units, Vector3 currentPos, Vector3 target) {
		return Vector3.Distance(currentPos, target) < units;
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
