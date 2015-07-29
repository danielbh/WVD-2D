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

		private void Fire() {
			playerActionController.Fire ();
		}

		private void Move () {
			playerActionController.Move ();
		}

		private bool FireButtonBeingTouched() {
			return playerActionController.FireButtonBeingTouched();
		}

		private bool DirectionButtonBeingTouched () {
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
