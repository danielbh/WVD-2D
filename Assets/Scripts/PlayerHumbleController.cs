// Humble Object used to test logic of code while decoupling code that cannot be mocked.

using UnityEngine;
using System;

namespace UnityTest
{
	[Serializable]
	public class PlayerHumbleController {
		
		public IPlayerActionController playerActionController;
		public bool firing = false;

		public readonly Vector3 initialFireDirection = Vector3.right;

		
		public void SetPlayerActionController (IPlayerActionController playerActionController) {
			this.playerActionController = playerActionController;
		}

		public void StartFiring() {
			firing = true;
			playerActionController.StartFiring();
		}

		public void StopFiring() {
			firing = false;
			playerActionController.StopFiring ();
		}

		private void Fire() {
			playerActionController.Fire ();
		}

		public void Move () {
			playerActionController.Move ();
		}
	}
}
