using UnityEngine;
using System.Collections;

public interface IRangedController {
	void Fire(Vector3 direction);
	Vector3 Aim();
}