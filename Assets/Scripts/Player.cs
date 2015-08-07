// Unmockable code object that controls how the joysticks connect with player

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IFireAimController, IMovementController {
	
	public PlayerController controller;
	public CNJoystick movementJoystick;
	public CNJoystick fireAimJoystick;
	
	public GameObject staff;
	public GameObject arrow;
	
	public float projectileSpeed = 8;
	public GameObject projectile;
	public float firingRate= 0.2f;
	public float turnSpeed = 50;
	
	public float moveSpeed= 2 ;

	// Use this for initialization
	public void OnEnable () {
		controller.SetFireAimController (this);
		controller.SetMovementController (this);
		fireAimJoystick.FingerTouchedEvent += StartFiring;
		fireAimJoystick.FingerLiftedEvent += StopFiring;
		
		// Set initial direction being faced
		FaceDirection(controller.initialFireDirection);
	}
	
	void OnDisable() {
		fireAimJoystick.FingerTouchedEvent -= StartFiring;
		fireAimJoystick.FingerLiftedEvent -= StopFiring;
	}
	
	void Update () {
		float h = GetMoveH(), v = GetMoveV();
		
		if (h != 0 || v != 0) {
			transform.position = controller.Move(transform.position, h, v, moveSpeed);
		}
	}
	
	public void StartFiring() {
		StartCoroutine ("FiringSequence"); 
	}
	
	public void StopFiring() {
		StopCoroutine("FiringSequence");
	}
	
	// Easier to test with Coroutine
	IEnumerator FiringSequence() {
		yield return new WaitForSeconds(0.00001f);
		
		for (;;) {
			controller.ApplyFire();
			yield return new WaitForSeconds(firingRate);
		}
	}
	
	#region IFireAimController implementation
	
	public void Fire () {
		GameObject beam = Instantiate(projectile, staff.transform.position, Quaternion.identity) as GameObject; 
		beam.GetComponent<Rigidbody2D>().velocity = controller.CalculateVelocity(projectileSpeed); // FIXME: Taste a bit like spaghetti code here recalling a method on controller. is this avoidable?
	}
	
	public float GetFireAimH() {
		return fireAimJoystick.GetAxis("Horizontal"); 
	}
	
	public float GetFireAimV() {
		return fireAimJoystick.GetAxis("Vertical");
	}
	
	#endregion
	
	#region IMovementController implementation
	
	public float GetMoveH() {
		return movementJoystick.GetAxis("Horizontal"); 
	}
	
	public float GetMoveV() {
		return movementJoystick.GetAxis("Vertical"); 
	}
	
	// FIXME: I had to sacrifice clarity here so I could make FaceDirection testable.
	public void FaceDirection(Vector3 newDirection) { 
		arrow.transform.rotation = controller.FaceDirection (newDirection, arrow.transform.rotation, turnSpeed);
	}
	
	#endregion	
}
