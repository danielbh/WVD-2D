using UnityEngine;
using System.Collections;

// TODO: Merge with EnemyAttackController somehow.
public interface IRangedController {
	void Fire(Vector3 direction);
	Vector3 Aim();
}