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
		var enemyMock = GetEnemyControllerMock(moveMock, GetAttackMock());

		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(2,0,0);
		float moveSpeed = 2;

		enemyMock.Move (currentPos, target,moveSpeed, new Quaternion(), 0);
		
		moveMock.Received(1).Move (currentPos, target, moveSpeed * Time.deltaTime);
	}

	[Test]
	[Category("Movement")]
	public void EnemyNeverGoesUnderWizard() {
		var moveMock = GetMoveMock();
		var enemyMock = GetEnemyControllerMock(moveMock, GetAttackMock());

		Vector3 currentPos = Vector3.zero;
		Vector3 target = Vector3.right;
		float moveSpeed = 2;
		
		Vector3 expected = enemyMock.Move (currentPos, target,moveSpeed, new Quaternion(), 0);

		Assert.AreEqual(expected, currentPos);
	}
	
	[Test]
	[Category("Hitpoints")]
	public void EnemyLosesHitPoints() {
		var hpMock = GetHitPointsMock();
		var enemyMock = GetEnemyControllerMock(hpMock);

		enemyMock.ReduceHitPoints (25, 100);

		hpMock.Received(1).ReduceHitPoints(25);
	}

	[Test]
	[Category("Hitpoints")]
	public void EnemyDiesOnZeroHitPoints() {
		var hpMock = GetHitPointsMock();
		var enemyMock = GetEnemyControllerMock(hpMock);
		
		enemyMock.ReduceHitPoints (25,25);
		
		hpMock.DidNotReceive().ReduceHitPoints(25);
		hpMock.Received(1).Destroy();
	}

	[Test]
	[Category("Combat")]
	public void EnemyAttacksWhenWithinRange() {

		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();
		var enemyMock = GetEnemyControllerMock(moveMock, attackMock);


		// Enemy not within range
		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(2,0,0);
		float moveSpeed = 2;

		enemyMock.Move (currentPos, target,moveSpeed, new Quaternion(), 0);

		// verify attack not called
		attackMock.DidNotReceive().Attack();

		// Make enemy in range
		target = Vector3.right;
		enemyMock.Move (currentPos, target,moveSpeed, new Quaternion(), 0);
		// Verify attack called
		attackMock.Received().Attack();
	}

	private IMoveController GetMoveMock () {
		return Substitute.For<IMoveController> ();
	}

	private IHitPointsController GetHitPointsMock () {
		return Substitute.For<IHitPointsController> ();
	}

	private IEnemyAttackController GetAttackMock () {
		return Substitute.For<IEnemyAttackController> ();
	}
	
	private EnemyController GetEnemyControllerMock (IMoveController move) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveController(move);
		
		return controller;
	}

	private EnemyController GetEnemyControllerMock (IHitPointsController hp) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetHitPointsController(hp);
		
		return controller;
	}

	private EnemyController GetEnemyControllerMock (IMoveController move, IEnemyAttackController attack) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveController(move);
		controller.SetAttackController(attack);
		
		return controller;
	}
}
