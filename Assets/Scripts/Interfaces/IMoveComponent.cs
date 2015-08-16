using UnityEngine;
using System.Collections;

public interface IMoveComponent {
	void FaceDirection(Quaternion newDirection);
	Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed);
}