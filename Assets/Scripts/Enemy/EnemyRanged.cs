using UnityEngine;
using System.Collections;

public class EnemyRanged : Enemy, IRangedComponent {	

	public Projectile projectile;
	
	override public void OnEnable() {
		controller.SetAttackComponent(this);
		base.OnEnable();
	}
	
	public void Attack(Vector3 direction) {
		Projectile beam = Instantiate(projectile, transform.position, Quaternion.identity) as Projectile; 
		beam.Fire(direction);
	}
	public Vector3 Aim() {
		Vector3 target = (player.transform.position - transform.position).normalized;
		return target;
	}
}
