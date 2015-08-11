using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

[TestFixture]
public class PlayerTests
{

	[Test]
	[Category("Firing and Aiming")]
	public void InitialFireDirectionIsAsExpected() {
		var attackMock = GetAttackMock();
		var playerController = GetPlayerControllerMock(attackMock);

		playerController.ApplyFire(new Quaternion(), 0);

		Assert.That(playerController.currentFireDirection == Vector3.right);
	}
		
	[Test]
	[Category("Firing and Aiming")]
	public void FaceDirectionCalledOnApplyFireCallWhenFireAimJoystickNotNeutral() {

		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();

		attackMock.GetFireAimAxes().Returns(Vector3.right);

		var playerController = GetPlayerControllerMock(moveMock, attackMock);

		playerController.ApplyFire(new Quaternion(), 0);

		moveMock.Received(1).FaceDirection (new Quaternion());
	}

	
	[Test]
	[Category("Firing and Aiming")]
	public void FaceDirectionCalledOnApplyFireNOTCalledWhenFireAimJoystickNotNeutral() {
		
		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();
		
		attackMock.GetFireAimAxes().Returns(Vector3.zero);
		
		var playerController = GetPlayerControllerMock(moveMock, attackMock);
		
		playerController.ApplyFire(new Quaternion(), 0);
		
		moveMock.DidNotReceive().FaceDirection (new Quaternion());
	}

	[Test]
	[Category("Firing and Aiming")]
	public void FireRecievesExpectedFireDirection() {

		var attackMock = GetAttackMock();
		
		attackMock.GetFireAimAxes().Returns(Vector3.right);
		
		var playerController = GetPlayerControllerMock(GetMoveMock(), attackMock);

		playerController.ApplyFire(new Quaternion(), 0);

		attackMock.Received(1).Fire(Vector3.right);
	}

	[Test]
	[Category("Firing and Aiming")]
	public void FireDirectionIsNormalizedAfterApplyFireCalled() {
		var attackMock = GetAttackMock();
		var moveMock = GetMoveMock();
		
		attackMock.GetFireAimAxes().Returns(new Vector3(1,1,0));
		
		var playerController = GetPlayerControllerMock(moveMock, attackMock);
		
		playerController.ApplyFire(new Quaternion(), 0);

		var actual = Vector3.SqrMagnitude(new Vector3(0.7f,0.7f,0) - playerController.currentFireDirection);
		var expected = 0.0002f;

		Assert.Greater (expected, actual);
	}
	

	[Test]
	[Category("Movement")]
	public void PlayerMovesDesignatedAmount() {

		Vector3 currentPos = Vector3.zero;
		Vector3 moveDirection = Vector3.right;
		float moveSpeed = 2;
		Vector3 target = moveDirection * moveSpeed + currentPos;

		var moveMock = GetMoveMock();
		var playerController = GetPlayerControllerMock(moveMock, GetAttackMock());
		playerController.Move (Vector3.zero, moveDirection,2, new Quaternion(), 0);

		moveMock.Received(1).Move (Vector3.zero, target, moveSpeed * Time.deltaTime);
	}

	[Test]
	[Category("Movement")]
	public void FaceDirectionInputOutputsExpected() {

		var moveMock = GetMoveMock();
		var playerController = GetPlayerControllerMock(moveMock, GetAttackMock());

		Quaternion oldRotation = new Quaternion(0,0,0.5f, 0.1f);
		Vector3 newDirection = Vector3.right;
		float targetAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.Euler( 0, 0, targetAngle );
		float turnSpeed = 2;
		float turnSpeedWithTime = turnSpeed * Time.deltaTime;

		playerController.FaceDirection(Vector3.right, oldRotation, turnSpeed);

		Quaternion expected = Quaternion.Slerp(oldRotation, rotation, turnSpeedWithTime);

		moveMock.Received(1).FaceDirection(expected);
	}

	[Test]
	[Category("Movement")]
	public void InMoveFunctionCallWhenMoveDirectionNotZeroAndAttackJoystickIsNeutralFaceDirectionNotCalled() {

		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();

		attackMock.GetFireAimAxes().Returns(Vector3.right);

		var playerController = GetPlayerControllerMock(moveMock, attackMock);
		
		playerController.Move (Vector3.zero, Vector3.right,2, new Quaternion(), 0);
		
		moveMock.DidNotReceive().FaceDirection(new Quaternion());
	}
	
	private IPlayerAttackController GetAttackMock () {
		return Substitute.For<IPlayerAttackController> ();
	}
	
	private IMoveController GetMoveMock () {
		return Substitute.For<IMoveController> ();
	}

	private PlayerController GetPlayerControllerMock (IPlayerAttackController fireAim) {
		var playerController = Substitute.For<PlayerController>();
		playerController.SetAttackController(fireAim);
		return playerController;
	}

	private PlayerController GetPlayerControllerMock (IMoveController move) {
		
		var playerController = Substitute.For<PlayerController>();
		playerController.SetMoveController(move);
		
		return playerController;
	}

	private PlayerController GetPlayerControllerMock (IMoveController move, IPlayerAttackController fireAim) {
		
		var playerController = Substitute.For<PlayerController>();
		playerController.SetMoveController(move);
		playerController.SetAttackController(fireAim);
		return playerController;
	}
}


