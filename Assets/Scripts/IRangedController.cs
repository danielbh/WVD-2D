using UnityEngine;
using System.Collections;

public interface IRangedController {
	void StartFiring();
	void StopFiring();
	void Fire(Vector3 direction);
	Vector3 Aim();
}