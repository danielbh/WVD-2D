using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IMoveController, IDestroyableController {

	public float moveSpeed = 2;
	public int maxHitPoints = 100;
	public float attackRate = 5;
	public int attackDamage = 25;
	public int hitPoints;

	protected Player player;

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
}
