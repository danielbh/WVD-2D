using UnityEngine;
using NSubstitute;
using NUnit.Framework;


	public class PlayerControllerTests
	{

		[Test]
		public void InitialFireDirectionIsAsExpected() {

			var player = GetPlayerMock();
			var playerMock = GetPlayerControllerMock(player);

			Assert.That(playerMock.initialFireDirection == Vector3.right);
		}

		[Test]
		public void FiringSetToTrueAndFalseWhenAppropriate() {
			var player = GetPlayerMock();
			var playerMock = GetPlayerControllerMock(player);

			playerMock.IsFiring();
			Assert.That(playerMock.firing == true);
			playerMock.IsFiring();
			Assert.That(playerMock.firing == false);
		}


		[Test]
		public void PlayerFaceDirectionCalled() {

			var player = GetPlayerMock();
			var playerMock = GetPlayerControllerMock(player);


		}
//
//		[Test]
//		public void PlayerMovesDesignatedAmount() {
//			var actionController = GetActionControllerMock();
//
//			actionController.GetMoveH().Returns (1);
//			actionController.GetMoveV().Returns (1);		}
//
		private IFiringController GetPlayerMock () {
			return Substitute.For<IFiringController> ();
		}

		private PlayerController GetPlayerControllerMock (IFiringController player) {

			var playerController = Substitute.For<PlayerController>();
			playerController.SetFiringController(player);

			return playerController;
		}
	}


 