using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

[TestFixture]
public class PlayerControllerTests
{

	[Test]
	[Category("Firing and Aiming")]
	public void InitialFireDirectionIsAsExpected() {
		var fireAimMock = GetFireAimMock();
		var playerController = GetPlayerControllerMock(fireAimMock);

		playerController.ApplyFire(new Quaternion(), 0);

		Assert.That(playerController.currentFireDirection == Vector3.right);
	}
		
	[Test]
	[Category("Firing and Aiming")]
	public void FaceDirectionCalledOnApplyFireCallWhenFireAimJoystickNotNeutral() {
		
		var fireAimMock = GetFireAimMock();

		fireAimMock.GetFireAimAxes().Returns(Vector3.right);

		var playerController = GetPlayerControllerMock(fireAimMock);

		playerController.FireAimJoystickNeutral().Returns(false);

		playerController.ApplyFire(new Quaternion(), 0);

		playerController.Received(1).FaceDirection (Vector3.right, new Quaternion(), 0);
	}

	[Test]
	[Category("Firing and Aiming")]
	public void FireAimJoystickInputOutputsExpected() {
		Assert.Fail ();
	}

	[Test]
	[Category("Firing and Aiming")]
	public void FireDirectionIsNormalizedAfterFireCalled() {
		Assert.Fail ();
	}

	[Test]
	[Category("Movement")]
	public void MovementJoystickInputOutputsExpected() {
		Assert.Fail();
	}
	
	[Test]
	[Category("Movement")]
	public void PlayerMovesDesignatedAmount() {
		Assert.Fail();
	}

	[Test]
	[Category("Movement")]
	public void CalculateVelocityInputOutputsExpected() {
		Assert.Fail();
	}

	[Test]
	[Category("Movement")]
	public void FaceDirectionInputOutputsExpected() {
		Assert.Fail();
	}

	[Test]
	[Category("Movement")]
	public void InMoveFunctionCallWhenMoveDirectionNotZeroAndFireAimJoystickIsNeutralFaceDirectionNotCalled() {
		Assert.Fail();
	}
	
	private IFireAimController GetFireAimMock () {
		return Substitute.For<IFireAimController> ();
	}
	
	private IMovementController GetMoveMock () {
		return Substitute.For<IMovementController> ();
	}
	
	private PlayerController GetPlayerControllerMock (IFireAimController fireAim) {
		var playerController = Substitute.For<PlayerController>();
		playerController.SetFireAimController(fireAim);
		return playerController;
	}

	private PlayerController GetPlayerControllerMock (IMovementController move) {
		
		var playerController = Substitute.For<PlayerController>();
		playerController.SetMovementController(move);
		
		return playerController;
	}

	private PlayerController GetPlayerControllerMock (IMovementController move, IFireAimController fireAim) {
		
		var playerController = Substitute.For<PlayerController>();
		playerController.SetMovementController(move);
		playerController.SetFireAimController(fireAim);
		return playerController;
	}
}


