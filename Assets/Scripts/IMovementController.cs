using UnityEngine;
using System.Collections;

public interface IMovementController {
	void FaceDirection(Quaternion newDirection);
}