using UnityEngine;
using NSubstitute;
using NUnit.Framework;

namespace UnityTest
{
	public class PlayerControllerTests
	{

		[Test]
		public void InitialFireDirectionIsAsExpected() {

			var actionController = GetActionControllerMock();
			var playerMock = GetPlayerHumbleControllerMock(actionController);

			Assert.That(playerMock.initialFireDirection == Vector3.right);
		}

		[Test]
		public void FiringSetToTrueAndFalseWhenAppropriate() {
			var playerMock = GetPlayerHumbleControllerMock(Substitute.For<IPlayerActionController> ());

			playerMock.StartFiring();
			Assert.That(playerMock.firing == true);
			playerMock.StopFiring();
			Assert.That(playerMock.firing == false);
		}

//
//		[Test]
//		public void PlayerMoveCalled() {
//			var actionController = GetActionControllerMock();
//			var humbleMock = GetPlayerHumbleControllerMock(actionController);
//					
//			actionController.Received(1).Move ();
//		}
//
//		[Test]
//		public void PlayerMovesDesignatedAmount() {
//			var actionController = GetActionControllerMock();
//
//			actionController.GetMoveH().Returns (1);
//			actionController.GetMoveV().Returns (1);		}
//
		private IPlayerActionController GetActionControllerMock () {
			return Substitute.For<IPlayerActionController> ();
		}

		private PlayerHumbleController GetPlayerHumbleControllerMock (IPlayerActionController actionController) {
			var playerHumble = Substitute.For<PlayerHumbleController>();
			playerHumble.SetPlayerActionController(actionController);

			return playerHumble;
		}
	}
}

 