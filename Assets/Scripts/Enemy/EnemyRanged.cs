using UnityEngine;
using System.Collections;

public class EnemyRanged : Enemy, IRangedComponent {	

	override public void OnEnable() {
		controller.SetAttackComponent(this);
		base.OnEnable();
	}

	//TODO: Attack when within range

	public void Attack(Vector3 direction) {
		print ("Enemy Ranged has attacked");
	}
	public Vector3 Aim() {return Vector3.zero;}
}
