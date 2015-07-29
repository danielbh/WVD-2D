using UnityEngine;
using System;

namespace UnityTest
{
	[Serializable]
	public class GUITouchHandler {
		
		public IPlayerActionController playerActionController;
		
		public void SetPlayerActionController (IPlayerActionController playerActionController) {
			this.playerActionController = playerActionController;
		}

		void Fire() {
			playerActionController.Fire ();
		}

		void Move () {
			playerActionController.Move ();
		}

		bool FireButtonBeingTouched() {
			return playerActionController.FireButtonBeingTouched();
		}

		bool DirectionButtonBeingTouched () {
			return playerActionController.DirectionButtonBeingTouched();
		}

		public void HandleTouches() {

			if (DirectionButtonBeingTouched()) {
				Move ();
			}

			if (FireButtonBeingTouched()) { 
				Fire ();
			}
		}

	}
}
