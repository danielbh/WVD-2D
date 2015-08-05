// Unmockable code object that controls how the joysticks connect with player

using UnityEngine;
using System.Collections;

namespace UnityTest
{
	
	public class PlayerController : MonoBehaviour, IPlayerActionController {
		
		public PlayerHumbleController humbleController;
		public CNJoystick movementJoystick;
		public CNJoystick fireAimJoystick;
		
		public float projectileSpeed = 10;
		public GameObject projectile;
		public float moveSpeed= 1 ;
		public float firingRate= 1;
		public float turnSpeed = 1;

		private GameObject arrow;

		// Use this for initialization
		void OnEnable () {
			humbleController.SetPlayerActionController (this);
			fireAimJoystick.FingerTouchedEvent += StartFiring;
			fireAimJoystick.FingerLiftedEvent += StopFiring;

			arrow = GameObject.Find ("arrow");
		}
		
		void Update () {
			humbleController.HandleJoystickInput();
		}
		
		private void StartFiring() {
			InvokeRepeating ("Fire", 0.00001f, firingRate);
		}
		
		private void StopFiring() {
			CancelInvoke("Fire");
		}
		
		#region IPlayerActionController implementation
		
		public void Fire () {
			Vector3 offset = new Vector3(0, 1, 0);
			GameObject beam = Instantiate(projectile, transform.position+offset, Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		}
		
		public void Move() {
			float h = GetMoveH(), v = GetMoveV();

			var currentPosition = transform.position;

			var rigidbody2D = GetComponent<Rigidbody2D>();
			
			// Apply horizontal velocity
			if (Mathf.Abs(h) > 0) {
				rigidbody2D.velocity = new Vector2(h * moveSpeed,  rigidbody2D.velocity.y);
				FaceDirection (rigidbody2D.velocity, currentPosition);
			}

			// Apply vertical velocity
			if (Mathf.Abs(v) > 0) {
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, v * moveSpeed);
				FaceDirection(rigidbody2D.velocity, currentPosition);
			}

			// Used to reset movement of player when there is no input.
			// TODO: is there a better way?
			if (h == 0 ||  v == 0) {
				rigidbody2D.velocity = new Vector2(0,0);
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
		
		public void FaceDirection(Vector3 moveToward, Vector3 currentPosition) {

			//  Find the angle needed to turn to face new direction player is moving.
			float targetAngle = Mathf.Atan2(moveToward.y, moveToward.x) * Mathf.Rad2Deg;

			// Rotate player in new direction
			arrow.transform.rotation = 
				Quaternion.Slerp( arrow.transform.rotation, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );
		}
	}
}
