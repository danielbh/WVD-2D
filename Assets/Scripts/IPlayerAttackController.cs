using UnityEngine;
using System.Collections;

// TODO: Merge with EnemyAttackController somehow.
public interface IPlayerAttackController {
	void Fire(Vector3 direction);
	Vector3 GetFireAimAxes();
}