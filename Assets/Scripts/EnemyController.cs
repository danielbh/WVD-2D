using UnityEngine;
using System;

[Serializable]
public class EnemyController : HumanoidController {

	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed, Quaternion oldRotation, float turnSpeed) {
		if (Vector3.Distance(currentPos, target) > 1) {
			return base.Move(currentPos, target, moveSpeed); 
		}

		return currentPos;
	}
}
