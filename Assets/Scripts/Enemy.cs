using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IMoveController {

	public EnemyController controller;
	public float moveSpeed = 2;

	private Player player;
	
	public void Start() {
		player = GameObject.FindObjectOfType<Player>();
	}

	public void OnEnable() {
		controller.SetMoveController (this);
	}

	public void Update() {
		if (player != null) {
			transform.position = controller.Move(transform.position, player.transform.position, moveSpeed, new Quaternion(), 0);
		}
	}

	#region IMoveController implementation
	
	public void FaceDirection(Quaternion newDirection) { 
	}
	
	// Wrapper for tests
	public Vector3 Move (Vector3 currentPos, Vector3 target, float deltaTime) {
		return Vector3.MoveTowards(currentPos, target, deltaTime);
	}

	#endregion

}
