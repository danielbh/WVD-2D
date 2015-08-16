using UnityEngine;
using System.Collections;

public class EnemyMelee : Enemy, IMeleeComponent {

	override public void OnEnable() {
		base.OnEnable();
		controller.SetAttackComponent(this);
	}

	#region IEnemyMeleeComponent implementation
	public void Attack() {
		player.GetComponent<HitPoints>().Hit(attackDamage);
	}
	#endregion


}
