using UnityEngine;
using System;

[Serializable]
public class EnemyRangedController : EnemyController {

	public IRangedComponent rangedAttackComponent;

	public void SetAttackComponent (IRangedComponent component) {
		rangedAttackComponent = component;
	}


	public new Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed/*, Quaternion oldRotation, float turnSpeed*/) {
		if (Vector3.Distance(currentPos, target) > 1) {
			if (attacking == true) {
				attacking = false;
				rangedAttackComponent.StopAttacking();			
			}
			return base.Move(currentPos, target, moveSpeed); 
		}
		
		if (attacking == false) {
			attacking = true;
			rangedAttackComponent.StartAttacking();
		} 
		return currentPos;
	}


	// FIXME: Lose test coverage if you do it this way need to change mock framework
//	override public void Attack() {
//		rangedAttackController.Attack(Vector3.right);
//	}
//
//	override public void StartAttacking() {
//		rangedAttackController.StartAttacking();
//	}
//	
//	override public void StopAttacking() {
//		rangedAttackController.StopAttacking();
//	}

	public new void AttemptHit(float coolDownPeriodInSeconds) {
		if (IsAttackReady()) {
			rangedAttackComponent.Attack(Vector3.right);
			attackCoolDownTime = CalcAttackCoolDown(coolDownPeriodInSeconds);
		} 
	}

}
