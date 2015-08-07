using UnityEngine;
using System.Collections;

public interface IMovementController {
	float GetMoveH ();
	float GetMoveV ();
	void FaceDirection(Vector3 direction);
}