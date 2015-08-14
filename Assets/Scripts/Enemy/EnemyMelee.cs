using UnityEngine;
using System.Collections;

public class EnemyMelee : Enemy, IMeleeController {

	[HideInInspector]
	public EnemyMeleeController controller;

	public void Start() {
		player = GameObject.FindObjectOfType<Player>();
	}

	public void OnEnable() {
		controller.SetMoveController (this);
		controller.SetHitPointsController(this);
		controller.SetAttackController(this);
		hitPoints = maxHitPoints;
	}

	public void Update() {
		if (player != null) {
			transform.position = controller.Move(transform.position, player.transform.position, moveSpeed, new Quaternion(), 0);
		}
	}

	public void Hit(int damage) {
		controller.ReduceHitPoints(damage, hitPoints);
	}
	
	#region IEnemyAttackController implementation
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

	public void Attack() {
		player.Hit (attackDamage);
	}
}
