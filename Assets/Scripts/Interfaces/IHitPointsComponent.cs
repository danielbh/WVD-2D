using UnityEngine;
using System.Collections;

public interface IHitPointsComponent {
	void ReduceHitPoints(int damage);
	void Destroy();
}
