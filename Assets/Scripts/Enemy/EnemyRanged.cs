using UnityEngine;
using System.Collections;

public class EnemyRanged : Enemy, IRangedComponent {	

	[HideInInspector]
	public EnemyRangedController rangedComponent;

	override public void OnEnable() {
		rangedComponent.SetMoveComponent (this);
		rangedComponent.SetAttackComponent(this);
	}

	override public void Update() {
		if (player != null) {
			transform.position = rangedComponent.Move(transform.position, player.transform.position, moveSpeed/*, new Quaternion(), 0*/);
		}
	}
	
	public void Attack(Vector3 direction) {
		print ("Enemy Ranged has attacked");
	}
	public Vector3 Aim() {return Vector3.zero;}

	new public  IEnumerator AttackSequence() {
		for (;;) {
			rangedComponent.AttemptHit(attackRate);
			yield return new WaitForSeconds(attackRate);
		}
	}
}
