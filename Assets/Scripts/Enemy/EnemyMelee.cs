using UnityEngine;
using System.Collections;

public class EnemyMelee : Enemy, IMeleeController {

	override public void OnEnable() {
		base.OnEnable();
		controller.SetAttackController(this);
	}

	#region IEnemyMeleeController implementation
	public void Attack() {
		player.Hit (attackDamage);
	}
	#endregion


}
