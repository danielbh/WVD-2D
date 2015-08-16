using UnityEngine;
using System.Collections;

public class EnemyRanged : Enemy, IRangedComponent {	

	override public void OnEnable() {
		controller.SetAttackComponent(this);
		base.OnEnable();
	}

	//TODO: Attack when within range
	override public void Update() {
		if (player != null) {
			transform.position = controller.MoveAsRanged(transform.position, player.transform.position, moveSpeed/*, new Quaternion(), 0*/);
		}
	}
	

	public void Attack(Vector3 direction) {
		print ("Enemy Ranged has attacked");
	}
	public Vector3 Aim() {return Vector3.zero;}
}
