using UnityEngine;
using System.Collections;

public class EnemyRanged : Enemy, IRangedController {	

	[HideInInspector]
	public EnemyRangedController rangedController;

	override public void OnEnable() {
		rangedController.SetMoveController (this);
		rangedController.SetAttackController(this);
	}

	override public void Update() {
		if (player != null) {
			transform.position = rangedController.Move(transform.position, player.transform.position, moveSpeed/*, new Quaternion(), 0*/);
		}
	}
	
	public void Attack(Vector3 direction) {
		print ("Enemy Ranged has attacked");
	}
	public Vector3 Aim() {return Vector3.zero;}

	new public  IEnumerator AttackSequence() {
		for (;;) {
			rangedController.AttemptHit(attackRate);
			yield return new WaitForSeconds(attackRate);
		}
	}
}
