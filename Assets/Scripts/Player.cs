// Unmockable code object that controls how the joysticks connect with player

using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour, IFiringController, IMovingController {
	
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
		controller.SetFiringController (this);
		controller.SetMovingController (this);
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
		controller.IsFiring(); 
	}
	
	public void StopFiring() {
		StopCoroutine("FiringSequence");
		controller.IsFiring();
	}
	
	// Easier to test with Coroutine
	IEnumerator FiringSequence() {
		yield return new WaitForSeconds(0.00001f);
		
		for (;;) {
			controller.ApplyFire();
			yield return new WaitForSeconds(firingRate);
		}
	}
	
	#region IFiringController implementation
	
	public void Fire () {
		GameObject beam = Instantiate(projectile, staff.transform.position, Quaternion.identity) as GameObject; 
		beam.GetComponent<Rigidbody2D>().velocity = controller.CalculateVelocity(projectileSpeed);
	}
	
	public float GetFireAimH() {
		return fireAimJoystick.GetAxis("Horizontal");  // TODO: Mock and test
	}
	
	public float GetFireAimV() {
		return fireAimJoystick.GetAxis("Vertical");  // TODO: Mock and test
	}
	
	#endregion
	
	#region IMovementController implementation
	
	public float GetMoveH() {
		return movementJoystick.GetAxis("Horizontal"); // TODO: Mock and test
	}
	
	public float GetMoveV() {
		return movementJoystick.GetAxis("Vertical"); // TODO: Mock and test
	}
	
	// FIXME: I had to sacrifice clarity here so I could make FaceDirection testable.
	public void FaceDirection(Vector3 newDirection) { // TODO: Test input and output
		arrow.transform.rotation = controller.FaceDirection (newDirection, arrow.transform.rotation, turnSpeed);
	}
	
	#endregion	
}
