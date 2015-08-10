using UnityEngine;
using System.Collections;

public interface IFireAimController {
	void Fire(Vector3 direction);
	Vector3 GetFireAimAxes();
}