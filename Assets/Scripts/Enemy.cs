using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IMoveController, IHitPointsController, IEnemyAttackController {

	[HideInInspector]
	public EnemyController controller;
	public float moveSpeed = 2;
	public int maxHitPoints = 100;
	public int hitPoints;
	public GameObject attackSprite;
	
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

	void OnTriggerEnter2D (Collider2D collider) {
		// TODO: Move to different class to be more consistent?
		int playerProjectileLayer = 10;
		if (collider.gameObject.layer == playerProjectileLayer) {
			controller.ReduceHitPoints(20, hitPoints);
			Destroy (collider.gameObject);
		}
	}
	
	#region IMoveController implementation

	public void ReduceHitPoints(int damage) { hitPoints -= damage; }

	// Test wrapper
	public void Destroy() { Destroy (gameObject); }

	#endregion

	#region IMoveController implementation
	
	public void FaceDirection(Quaternion newDirection) { }
	
	// Wrapper for tests
	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed) {
		return Vector3.MoveTowards(currentPos, target, moveSpeed);
	}

	#endregion

	#region IEnemyAttackController implementation

	public void Attack() {
		print ("Enemy Attacking!");
		// spawn attack sprite
		Instantiate(attackSprite, player.transform.position, Quaternion.identity); 
		// deal damage to Player
	}

	#endregion

}
