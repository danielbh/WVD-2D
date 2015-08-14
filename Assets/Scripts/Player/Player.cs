// Unmockable code object that controls how the joysticks connect with player

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IRangedController, IMoveController {

	[HideInInspector]
	public PlayerController controller;
	public CNJoystick moveJoystick;
	public CNJoystick fireAimJoystick;
	
	public GameObject staff;
	public GameObject arrow;
	
	public float projectileSpeed = 8;
	public GameObject projectile;
	public float firingRate= 0.2f;
	public float turnSpeed = 50;
	public float moveSpeed= 2 ;
	public GameObject damageTakenSprite;
	public bool movementAcceleration = false; // The intensity of joystick increases the speed at which the wizard moves
	
	// Use this for initialization
	public void OnEnable () {
		controller.SetAttackController (this);
		controller.SetMoveController (this);
		fireAimJoystick.FingerTouchedEvent += StartFiring;
		fireAimJoystick.FingerLiftedEvent += StopFiring;
	}
	
	void OnDisable() {
		fireAimJoystick.FingerTouchedEvent -= StartFiring;
		fireAimJoystick.FingerLiftedEvent -= StopFiring;
	}
	
	void Update () {
		float h = moveJoystick.GetAxis("Horizontal"), v = moveJoystick.GetAxis("Vertical");
		
		if (h != 0 || v != 0) {
			transform.position = controller.Move(transform.position, new Vector2(h, v), moveSpeed, arrow.transform.rotation, turnSpeed);
		}
	}

	public  IEnumerator FiringSequence() {
		yield return new WaitForSeconds(0.00001f);
		
		for (;;) {
			controller.ApplyFire(arrow.transform.rotation, turnSpeed);
			yield return new WaitForSeconds(firingRate);
		}
	}

	public void Hit(int damage) {
		GameObject sprite = Instantiate(damageTakenSprite, transform.position, Quaternion.identity) as GameObject; 
		sprite.transform.parent = this.gameObject.transform;
	}
	
	#region IFireController implementation

	public void StartFiring() {
		StartCoroutine ("FiringSequence"); 
	}
	
	public void StopFiring() {
		StopCoroutine("FiringSequence");
	}

	public void Fire (Vector3 direction) {
		GameObject beam = Instantiate(projectile, staff.transform.position, Quaternion.identity) as GameObject; 
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x * projectileSpeed , direction.y * projectileSpeed,0); 
	}
	
	public Vector3 Aim() {
		return new Vector3(fireAimJoystick.GetAxis("Horizontal"), fireAimJoystick.GetAxis("Vertical"), 0);
	}

	#endregion
	
	#region IMoveController implementation

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
