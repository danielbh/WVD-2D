using UnityEngine;
using System.Collections;

public class EnemyMelee : MonoBehaviour, IMoveController, IHitPointsController, IMeleeController {

	[HideInInspector]
	public EnemyMeleeController controller;
	public float moveSpeed = 2;
	public int maxHitPoints = 100;
	public float attackRate = 5;
	public int attackDamage = 25;
	public int hitPoints;

	private Player player;
	
	public void Start() {
		player = GameObject.FindObjectOfType<Player>();
	}

	public void OnEnable() {
		controller.SetMoveController (this);
		controller.SetHitPointsController(this);
		controller.SetAttackController(this);
		hitPoints = maxHitPoints;
	}

	public void Update() {
		if (player != null) {
			transform.position = controller.Move(transform.position, player.transform.position, moveSpeed, new Quaternion(), 0);
		}
	}

	public void Hit(int damage) {
		controller.ReduceHitPoints(damage, hitPoints);
	}

	#region IMoveController implementation

	public void ReduceHitPoints(int damage) { hitPoints -= damage; }

	// Test wrapper
	public void Destroy() { Destroy (gameObject); }

	#endregion

	#region IMoveController implementationrun
	
	public void FaceDirection(Quaternion newDirection) { }
	
	// Wrapper for tests
	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed) {
		return Vector3.MoveTowards(currentPos, target, moveSpeed);
	}

	#endregion

	#region IEnemyAttackController implementation
	public void StartAttacking() {
		StartCoroutine ("AttackingSequence"); 
	}
	
	public void StopAttacking() {
		StopCoroutine("AttackingSequence");
	}
	#endregion

	public  IEnumerator AttackingSequence() {
		for (;;) {
			controller.AttemptHit(attackRate);
			yield return new WaitForSeconds(attackRate);
		}
	}

	public void Attack() {
		player.Hit (attackDamage);
	}
}
