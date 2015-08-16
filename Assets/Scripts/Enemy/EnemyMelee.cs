using UnityEngine;
using System.Collections;

public class EnemyMelee : Enemy, IMeleeComponent {

	override public void OnEnable() {
		controller.SetAttackComponent(this);
		base.OnEnable();
	}

	#region IEnemyMeleeComponent implementation
	public void Attack() {
		player.GetComponent<HitPoints>().Hit(attackDamage);
	}
	#endregion


}
