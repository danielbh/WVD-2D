using UnityEngine;
using System;

[Serializable]
public class HitPointsController  {

	public IHitPointsComponent hpComponent;

	public void SetComponent (IHitPointsComponent component) {
		hpComponent = component;
	}

	public void ReduceHitPoints (int damage, int currentHP) {
		if (damage >= currentHP) {
			hpComponent.Kill();
		} else {
			hpComponent.ReduceHitPoints(damage);
		}
	}
}
