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
		public float movementSpeed= 1 ;
		public float firingRate= 1f;

		private float h, v; 

		// Use this for initialization
		void OnEnable () {
			humbleController.SetPlayerActionController (this);
			fireAimJoystick.FingerTouchedEvent += StartFiring;
			fireAimJoystick.FingerLiftedEvent += StopFiring;
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
			float h = 0, v = 0;

			var rigidbody2D = GetComponent<Rigidbody2D>();
			
			h = GetMoveH();
			v = GetMoveV();
			
			// Apply horizontal velocity
			if (Mathf.Abs(h) > 0) {
				rigidbody2D.velocity = new Vector2(h * movementSpeed,  rigidbody2D.velocity.y);
			}
			
			// Apply vertical velocity
			if (Mathf.Abs(v) > 0) {
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, v * movementSpeed);
			}

			if (GetMoveH() == 0 ||  GetMoveV() == 0) {
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
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

		void FaceDirection () {	
		}
	
	}
}
