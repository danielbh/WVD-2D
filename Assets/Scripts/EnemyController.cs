using UnityEngine;
using System;

[Serializable]
public class EnemyController : HumanoidController {

	public Vector3 Move (Vector3 currentPos, Vector3 moveDirection, float moveSpeed, Quaternion oldRotation, float turnSpeed) {
		return moveController.Move (currentPos, moveDirection, moveSpeed * Time.deltaTime); 
	}

}
