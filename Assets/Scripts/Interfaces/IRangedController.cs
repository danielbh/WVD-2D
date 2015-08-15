using UnityEngine;
using System.Collections;

public interface IRangedController: IAttackController {
	void Attack(Vector3 direction);
	Vector3 Aim();
}