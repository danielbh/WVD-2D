using UnityEngine;
using System.Collections;

namespace UnityTest
{
	
	public class PlayerInputController : MonoBehaviour, IPlayerActionController {
		
		public GUITouchHandler touchHandler;
		private RaycastHit hit;
		private Ray ray;
		
		// Use this for initialization
		void OnEnable () {
			touchHandler.SetPlayerActionController (this);
		}
		
		void Update () {
			foreach (Touch touch in Input.touches) {
				ray = Camera.main.ScreenPointToRay(touch.position);
				touchHandler.HandleTouches();
			}
		}
		
		#region IPlayerActionController implementation

		public void Fire() {
			GameObject.Find("Player").GetComponent<PlayerController>().Fire();
		}
		
		public void Move () {
			hit.collider.gameObject.GetComponent<DirectionButton>().MovePlayerInDirection();
		}
		
		public bool FireButtonBeingTouched () {
			return Physics.Raycast(ray, out hit) && hit.transform.gameObject.name == "Fire";
		}
		public bool DirectionButtonBeingTouched () {
			return Physics.Raycast(ray, out hit) && hit.transform.parent.name == "Movement Controls";
		}
		
		#endregion
	}
}
