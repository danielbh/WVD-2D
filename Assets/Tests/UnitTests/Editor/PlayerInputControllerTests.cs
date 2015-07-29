using NSubstitute;
using NUnit.Framework;

namespace UnityTest
{
	public class PlayerInputControllerTests
	{
		[Test]
		public void PlayerCanMoveAndFireAtTheSameTime() {

			var actionController = GetActionControllerMock();
			var touchHandler = GetGUITouchHandlerMock(actionController);
			actionController.DirectionButtonBeingTouched().Returns(true);
			actionController.FireButtonBeingTouched().Returns(true);

			touchHandler.HandleTouches();

			actionController.Received(1).Move ();
			actionController.Received(1).Fire ();
		}

		[Test]
		public void TheDpadStillWorksIfPlayerMovesFingerOutsideOfDpadAfterPressingButDoesNotRemoveItFromTheScreen() {
			//Press down on Dpad up
			// Figure is moved outisde of Dpad Sprite
			// Assert Dpad is still being pressed
		}

		private IPlayerActionController GetActionControllerMock () {
			return Substitute.For<IPlayerActionController> ();
		}

		private GUITouchHandler GetGUITouchHandlerMock (IPlayerActionController actionController) {
			var touchHandler = Substitute.For<GUITouchHandler>();
			touchHandler.SetPlayerActionController(actionController);

			return touchHandler;
		}
	}
}

 