﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IAttackController, IMoveController, IDestroyableController {

	[HideInInspector]
	public EnemyController controller;
	public float moveSpeed = 2;
	public int maxHitPoints = 100;
	public float attackRate = 5;
	public int attackDamage = 25;
	public int hitPoints;

	protected Player player;

	public void Start() {
		player = GameObject.FindObjectOfType<Player>();
		hitPoints = maxHitPoints;
	}
	
	virtual public void OnEnable() {
		controller.SetMoveController (this);
		controller.SetHitPointsController(this);
	}
	
	virtual public void Update() {
		if (player != null) {
			transform.position = controller.Move(transform.position, player.transform.position, moveSpeed/*, new Quaternion(), 0*/);
		}
	}
	
	public void Hit(int damage) {
		controller.ReduceHitPoints(damage, hitPoints);
	}
	
	#region IMoveController implementation
	
	public void FaceDirection(Quaternion newDirection) { }
	
	// Wrapper for tests
	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed) {
		return Vector3.MoveTowards(currentPos, target, moveSpeed);
	}
	
	#endregion

	#region IDestroyableController implementation
	
	public void ReduceHitPoints(int damage) { hitPoints -= damage; }
	
	// Test wrapper
	public void Destroy() { Destroy (gameObject); }
	
	#endregion

	#region IAttackController implementation
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
