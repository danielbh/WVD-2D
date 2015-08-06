// Humble Object used to test logic of code while decoupling code that cannot be mocked.

using UnityEngine;
using System;

namespace UnityTest
{
	[Serializable]
	public class PlayerHumbleController {
		
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

		public void HandleJoystickInput() {
			Move ();
		}
	}
}
