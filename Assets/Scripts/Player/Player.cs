// Unmockable code object that controls how the joysticks connect with player

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IRangedComponent, IMoveComponent {

	[HideInInspector]
	public PlayerController controller;
	public CNJoystick moveJoystick;
	public CNJoystick fireAimJoystick;
	
	public GameObject staff;
	public GameObject arrow;
	
	public Projectile projectile;
	public float firingRate= 0.2f;
	public float turnSpeed = 50;
	public float moveSpeed= 2 ;
	public bool movementAcceleration = false; // The intensity of joystick increases the speed at which the wizard moves
	
	// Use this for initialization
	public void OnEnable () {
		controller.SetAttackComponent (this);
		controller.SetMoveComponent (this);
		fireAimJoystick.FingerTouchedEvent += StartAttacking;
		fireAimJoystick.FingerLiftedEvent += StopAttacking;
	}
	
	void OnDisable() {
		fireAimJoystick.FingerTouchedEvent -= StartAttacking;
		fireAimJoystick.FingerLiftedEvent -= StopAttacking;
	}
	
	void Update () {
		float h = moveJoystick.GetAxis("Horizontal"), v = moveJoystick.GetAxis("Vertical");
		
		if (h != 0 || v != 0) {
			transform.position = controller.Move(transform.position, new Vector2(h, v), moveSpeed, arrow.transform.rotation, turnSpeed);
		}
	}

	public  IEnumerator AttackSequence() {
		yield return new WaitForSeconds(0.00001f);
		
		for (;;) {
			controller.ApplyFire(arrow.transform.rotation, turnSpeed);
			yield return new WaitForSeconds(firingRate);
		}
	}

	#region IFireComponent implementation

	public void StartAttacking() {
		StartCoroutine ("AttackSequence"); 
	}
	
	public void StopAttacking() {
		StopCoroutine("AttackSequence");
	}

	public void Attack (Vector3 direction) {
		Projectile beam = Instantiate(projectile, staff.transform.position, Quaternion.identity) as Projectile; 
		beam.Fire(direction);
	}
	
	public Vector3 Aim() {
		return new Vector3(fireAimJoystick.GetAxis("Horizontal"), fireAimJoystick.GetAxis("Vertical"), 0);
	}

	#endregion
	
	#region IMoveComponent implementation

	public void FaceDirection(Quaternion newDirection) { 
		arrow.transform.rotation = newDirection;
	}

	// Wrapper for tests
	public Vector3 Move (Vector3 currentPos, Vector3 target, float moveSpeed) {

		if (movementAcceleration) {
			return Vector3.Lerp (currentPos, target, moveSpeed);
		}

		return Vector3.MoveTowards (currentPos, target, moveSpeed);
	}

	#endregion	
}
