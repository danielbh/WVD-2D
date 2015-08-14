using UnityEngine;
using System.Collections;

public class EnemyMelee : Enemy, IMeleeController {

	override public void OnEnable() {
		base.OnEnable();
		controller.SetAttackController(this);
	}
	
	#region IEnemyAttackController implementation
	public void Attack() {
		player.Hit (attackDamage);
	}
	
	public void StartAttacking() {
		StartCoroutine ("AttackingSequence"); 
	}
	
	public void StopAttacking() {
		StopCoroutine("AttackingSequence");
	}
	#endregion

	public  IEnumerator AttackingSequence() {
		for (;;) {
			controller.AttemptHit(attackRate);
			yield return new WaitForSeconds(attackRate);
		}
	}


}
