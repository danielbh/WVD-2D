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
		public void OnEnable () {
			humbleController.SetPlayerActionController (this);
			fireAimJoystick.FingerTouchedEvent += humbleController.StartFiring;
			fireAimJoystick.FingerLiftedEvent += humbleController.StopFiring;

			// Set initial direction being faced
			FaceDirection(humbleController.initialFireDirection);
			print (fireDirection);
		}

		void OnDisable() {
			fireAimJoystick.FingerTouchedEvent -= humbleController.StartFiring;
			fireAimJoystick.FingerLiftedEvent -= humbleController.StopFiring;
		}
		
		void Update () {
			humbleController.Move();
		}

		#region IPlayerActionController implementation

		public void StartFiring() {
			InvokeRepeating ("Fire", 0.00001f, firingRate); 
			firing = humbleController.firing;
		}
		
		public void StopFiring() {
			CancelInvoke("Fire");
			firing = humbleController.firing;
		}
		
		public void Fire () {

			// Second condition: Makes sure that last fire direction is persisted if a new one isn't entered when stationary firing
			if (firing == true && GetFireAimH() != 0 && GetFireAimV() != 0) { // TODO: Check conditions
				// FIXME: Turns choppy needs to be made smooth.
				FaceDirection (new Vector3(GetFireAimH(), GetFireAimV())); // TODO: Test function call
			} 

			// Convert to unit vector so projectiles are all going the same speed.
			fireDirection.Normalize(); // TODO: Test normalized

			GameObject beam = Instantiate(projectile, staff.transform.position, Quaternion.identity) as GameObject; // TODO: Test instantiated
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3(fireDirection.x * projectileSpeed , fireDirection.y * projectileSpeed,0); // TODO: Test velocity set correctly
		}
		
		public void Move() {
			var currentPosition = transform.position; 
			Vector3 moveDirection = new Vector2(GetMoveH(),  GetMoveV());
			Vector3 target = moveDirection * moveSpeed + currentPosition;

			transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime); // TODO: Test move position is now correct

			if (moveDirection != Vector3.zero && firing != true) { // TODO: Test conditions
				FaceDirection (moveDirection); // TODO: Test called AND function gives right output
			}
		}
		
		public float GetFireAimH() {
			return fireAimJoystick.GetAxis("Horizontal"); // TODO: Mock and test
		}
		
		public float GetFireAimV() {
			return fireAimJoystick.GetAxis("Vertical"); // TODO: Mock and test
		}
		
		public float GetMoveH() {
			return movementJoystick.GetAxis("Horizontal"); // TODO: Mock and test
		}
		
		public float GetMoveV() {
			return movementJoystick.GetAxis("Vertical"); // TODO: Mock and test
		}
		#endregion
		
		public void FaceDirection(Vector3 faceDirection) { // TODO: Test input and output

			fireDirection = faceDirection;
		
			//  Find the angle needed to turn to face new direction player is moving.
			float targetAngle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;

			// Rotate player in new direction
			arrow.transform.rotation = 
				Quaternion.Slerp( arrow.transform.rotation, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );
		}
	}
}
