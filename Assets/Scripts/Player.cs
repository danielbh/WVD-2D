﻿// Unmockable code object that controls how the joysticks connect with player

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
	}
	
	void OnDisable() {
		fireAimJoystick.FingerTouchedEvent -= StartFiring;
		fireAimJoystick.FingerLiftedEvent -= StopFiring;
	}
	
	void Update () {
		float h = movementJoystick.GetAxis("Horizontal"), v = movementJoystick.GetAxis("Vertical");
		
		if (h != 0 || v != 0) {
			transform.position = controller.Move(transform.position, new Vector2(h, v), moveSpeed, arrow.transform.rotation, turnSpeed);
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
			controller.ApplyFire(arrow.transform.rotation, turnSpeed);
			yield return new WaitForSeconds(firingRate);
		}
	}
	
	#region IFireAimController implementation
	
	public void Fire (Vector3 direction) {
		GameObject beam = Instantiate(projectile, staff.transform.position, Quaternion.identity) as GameObject; 
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x * projectileSpeed , direction.y * projectileSpeed,0); 
	}
	
	public Vector3 GetFireAimAxes() {
		return new Vector3(fireAimJoystick.GetAxis("Horizontal"), fireAimJoystick.GetAxis("Vertical"), 0);
	}

	#endregion
	
	#region IMovementController implementation
	public void FaceDirection(Quaternion newDirection) { 
		arrow.transform.rotation = newDirection;
	}
	
	#endregion	
}
