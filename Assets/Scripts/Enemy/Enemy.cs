using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IAttackComponent, IMoveComponent {

	[HideInInspector]
	public EnemyController controller;
	public float moveSpeed = 2;
	public float attackRate = 5;
	public int attackDamage = 25;
	
	protected Player player;

	public void Start() {
		player = GameObject.FindObjectOfType<Player>();
	}
	
	virtual public void OnEnable() {
		controller.SetMoveComponent (this);
		controller.SetAttackComponent(this);
	}
	
	virtual public void Update() {
		if (player != null) {
			transform.position = controller.Move(transform.position, player.transform.position, moveSpeed/*, new Quaternion(), 0*/);
		}
	}

	#region IMoveComponent implementation
	
	public void FaceDirection(Quaternion newDirection) { }
	
	// Wrapper for tests
	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed) {
		return Vector3.MoveTowards(currentPos, target, moveSpeed);
	}
	
	#endregion

	#region IAttackComponent implementation
	public void StartAttacking() {
		StartCoroutine ("AttackSequence"); 
	}
	
	public void StopAttacking() {
		StopCoroutine("AttackSequence");
	}
	#endregion

	public  IEnumerator AttackSequence() {
		for (;;) {
			controller.AttemptHit(attackRate);
			yield return new WaitForSeconds(attackRate);
		}
	}	
}
