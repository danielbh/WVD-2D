using UnityEngine;
using System.Collections;

public interface IRangedComponent: IAttackComponent {
	void Attack(Vector3 direction);
	Vector3 Aim();
}