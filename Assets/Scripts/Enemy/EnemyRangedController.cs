using UnityEngine;
using System;

[Serializable]
public class EnemyRangedController : EnemyController {

	public IRangedController rangedAttackController;

	public void SetAttackController (IRangedController controller) {
		rangedAttackController = controller;
	}


	public new Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed/*, Quaternion oldRotation, float turnSpeed*/) {
		if (Vector3.Distance(currentPos, target) > 1) {
			if (attacking == true) {
				attacking = false;
				rangedAttackController.StopAttacking();			
			}
			return base.Move(currentPos, target, moveSpeed); 
		}
		
		if (attacking == false) {
			attacking = true;
			rangedAttackController.StartAttacking();
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
			rangedAttackController.Attack(Vector3.right);
			attackCoolDownTime = CalcAttackCoolDown(coolDownPeriodInSeconds);
		} 
	}

}
