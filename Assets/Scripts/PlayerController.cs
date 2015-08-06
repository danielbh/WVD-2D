// Unmockable code object that controls how the joysticks connect with player

using UnityEngine;
using System.Collections;

namespace UnityTest
{
	
	public class PlayerController : MonoBehaviour, IPlayerActionController {
		
		public PlayerHumbleController humbleController;
		public CNJoystick movementJoystick;
		public CNJoystick fireAimJoystick;

		public GameObject staff;
		public GameObject arrow;

		public float projectileSpeed = 8;
		public GameObject projectile;
		public float moveSpeed= 2 ;
		public float firingRate= 0.2f;
		public float turnSpeed = 50;
		public Vector3 fireDirection;

		private bool firing = false;

		// Use this for initialization
		void OnEnable () {
			humbleController.SetPlayerActionController (this);
			fireAimJoystick.FingerTouchedEvent += StartFiring;
			fireAimJoystick.FingerLiftedEvent += StopFiring;

			// Set initial direction being faced
			FaceDirection(Vector3.right);
		}

		void OnDisable() {
			fireAimJoystick.FingerTouchedEvent -= StartFiring;
			fireAimJoystick.FingerLiftedEvent -= StopFiring;
		}
		
		void Update () {
			humbleController.HandleJoystickInput();
		}
		
		private void StartFiring() {
			InvokeRepeating ("Fire", 0.00001f, firingRate);
			firing = true;
		}
		
		private void StopFiring() {
			CancelInvoke("Fire");
			firing = false;
		}
		
		#region IPlayerActionController implementation
		
		public void Fire () {

			// Second condition: Makes sure that last fire direction is persisted if a new one isn't entered when stationary firing
			if (firing == true && GetFireAimH() != 0 && GetFireAimV() != 0) {
				// FIXME: Turns choppy needs to be made smooth.
				FaceDirection (new Vector3(GetFireAimH(), GetFireAimV()));
			} 

			// Convert to unit vector so projectiles are all going the same speed.
			fireDirection.Normalize();

			GameObject beam = Instantiate(projectile, staff.transform.position, Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3(fireDirection.x * projectileSpeed , fireDirection.y * projectileSpeed,0);
		}
		
		public void Move() {
			var currentPosition = transform.position;
			Vector3 moveDirection = new Vector2(GetMoveH(),  GetMoveV());
			Vector3 target = moveDirection * moveSpeed + currentPosition;

			transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);

			if (moveDirection != Vector3.zero && firing != true) {
				FaceDirection (moveDirection);
			}
		}
		
		public float GetFireAimH() {
			return fireAimJoystick.GetAxis("Horizontal");
		}
		
		public float GetFireAimV() {
			return fireAimJoystick.GetAxis("Vertical");
		}
		
		public float GetMoveH() {
			return movementJoystick.GetAxis("Horizontal");
		}
		
		public float GetMoveV() {
			return movementJoystick.GetAxis("Vertical");
		}
		#endregion
		
		public void FaceDirection(Vector3 faceDirection) {

			fireDirection = faceDirection;
		
			//  Find the angle needed to turn to face new direction player is moving.
			float targetAngle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;

			// Rotate player in new direction
			arrow.transform.rotation = 
				Quaternion.Slerp( arrow.transform.rotation, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );
		}
	}
}
