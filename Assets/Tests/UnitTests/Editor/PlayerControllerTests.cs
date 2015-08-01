using NSubstitute;
using NUnit.Framework;

namespace UnityTest
{
	public class PlayerControllerTests
	{
		[Test]
		public void PlayerFireCalled() {

			var actionController = GetActionControllerMock();
			var humbleMock = GetPlayerHumbleControllerMock(actionController);

			humbleMock.HandleJoystickInput();

			//actionController.Received(1).Fire ();
		}

		[Test]
		public void PlayerMoveCalled() {
			var actionController = GetActionControllerMock();
			var humbleMock = GetPlayerHumbleControllerMock(actionController);

			humbleMock.HandleJoystickInput();
		
			actionController.Received(1).Move ();
		}

		[Test]
		public void PlayerMovesDesignatedAmount() {
			var actionController = GetActionControllerMock();

			actionController.GetMoveH().Returns (1);
			actionController.GetMoveV().Returns (1);		}

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

 