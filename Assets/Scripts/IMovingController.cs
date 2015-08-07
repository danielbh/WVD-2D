using UnityEngine;
using System.Collections;

public interface IMovingController {
	//void Move (Vector3 newPos);
	float GetMoveH ();
	float GetMoveV ();
	void FaceDirection(Vector3 direction);
}