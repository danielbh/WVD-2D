using UnityEngine;
using System.Collections;

public interface IMoveController {
	void FaceDirection(Quaternion newDirection);
	Vector3 VectorLerp (Vector3 currentPos, Vector3 target, float deltaTime);
}