using UnityEngine;
using System.Collections;

public class HitPoints :   MonoBehaviour, IHitPointsComponent {

	[HideInInspector]
	public HitPointsController controller;
	public GameObject damageTakenSprite;
	public int maxHitPoints = 100;
	public int hitPoints;

	public void OnEnable() {
		controller.SetComponent(this);
		hitPoints = maxHitPoints;
	}

	#region IHitPontsComponent implementation

	public void Hit(int damage) {
		AnimateHit();
		controller.ReduceHitPoints(damage, hitPoints);
	}

	public void Destroy() { Destroy (gameObject); }

	#endregion

	public void AnimateHit() {
		GameObject sprite = Instantiate(damageTakenSprite, transform.position, Quaternion.identity) as GameObject; 
		sprite.transform.parent = this.gameObject.transform;
	}

	public void ReduceHitPoints(int damage) { hitPoints -= damage; }
}
