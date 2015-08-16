using UnityEngine;
using System.Collections;

public class HitPoints :   MonoBehaviour, IHitPointsComponent {

	[HideInInspector]
	public HitPointsController controller;
	public GameObject damageTakenSprite;
	public int hitPoints = 100;
	public bool invulnerable = false;

	public void OnEnable() {
		controller.SetComponent(this);
	}

	#region IHitPointsComponent implementation

	public void Hit(int damage) {
		AnimateHit();
		if (invulnerable == false) {
			controller.ReduceHitPoints(damage, hitPoints);
		}
	}

	// TODO: Add death triggers game over for player
	public void Kill() { Destroy (gameObject); }

	#endregion

	public void AnimateHit() {
		GameObject sprite = Instantiate(damageTakenSprite, transform.position, Quaternion.identity) as GameObject; 
		sprite.transform.parent = this.gameObject.transform;
	}

	public void ReduceHitPoints(int damage) { hitPoints -= damage; }
}
