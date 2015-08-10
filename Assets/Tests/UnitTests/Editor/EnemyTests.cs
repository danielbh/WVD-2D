using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

[TestFixture]
public class EnemyTests  {

	[Test]
	[Category("Movement")]
	public void MoveToPoint() {
		var moveMock = GetMoveMock();
		var enemyMock = GetEnemyControllerMock(moveMock);

		Vector3 moveDirection = Vector3.right;
		float moveSpeed = 2;

		enemyMock.Move (Vector3.zero, moveDirection,moveSpeed, new Quaternion(), 0);
		
		moveMock.Received(1).Move (Vector3.zero, Vector2.right, moveSpeed * Time.deltaTime);
	}	

	[Test]
	[Category("Hitpoints")]
	public void EnemyLosesHitPoints() {
		Assert.Fail();
	}

	[Test]
	[Category("Hitpoints")]
	public void EnemyDiesOnZeroHitPoints() {
		Assert.Fail();
	}

	private IMoveController GetMoveMock () {
		return Substitute.For<IMoveController> ();
	}
	
	private EnemyController GetEnemyControllerMock (IMoveController move) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveController(move);
		
		return controller;
	}
}
