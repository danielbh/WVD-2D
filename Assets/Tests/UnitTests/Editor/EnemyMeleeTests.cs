using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

[TestFixture]
public class EnemyMeleeTests  {

	[Test]
	[Category("Movement")]
	public void MoveToPoint() {
		var moveMock = GetMoveMock();
		var enemyMock = GetEnemyMeleeControllerMock(moveMock, GetAttackMock());

		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(2,0,0);
		float moveSpeed = 2;

		enemyMock.Move (currentPos, target,moveSpeed, new Quaternion(), 0);
		
		moveMock.Received(1).Move (currentPos, target, moveSpeed * Time.deltaTime);
	}

	[Test]
	[Category("Movement")]
	public void EnemyMeleeNeverGoesUnderWizard() {
		var moveMock = GetMoveMock();
		var enemyMock = GetEnemyMeleeControllerMock(moveMock, GetAttackMock());

		Vector3 currentPos = Vector3.zero;
		Vector3 target = Vector3.right;
		float moveSpeed = 2;
		
		Vector3 expected = enemyMock.Move (currentPos, target,moveSpeed, new Quaternion(), 0);

		Assert.AreEqual(expected, currentPos);
	}
	
	[Test]
	[Category("Hitpoints")]
	public void EnemyMeleeLosesHitPoints() {
		var hpMock = GetHitPointsMock();
		var enemyMock = GetEnemyMeleeControllerMock(hpMock);

		enemyMock.ReduceHitPoints (25, 100);

		hpMock.Received(1).ReduceHitPoints(25);
	}

	[Test]
	[Category("Hitpoints")]
	public void EnemyMeleeDiesOnZeroHitPoints() {
		var hpMock = GetHitPointsMock();
		var enemyMock = GetEnemyMeleeControllerMock(hpMock);
		
		enemyMock.ReduceHitPoints (25,25);
		
		hpMock.DidNotReceive().ReduceHitPoints(25);
		hpMock.Received(1).Destroy();
	}

	[Test]
	[Category("Combat")]
	public void EnemyMeleeAttacksWhenWithinRange() {

		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();
		var enemyMock = GetEnemyMeleeControllerMock(moveMock, attackMock);


		// EnemyMelee not within range
		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(2,0,0);
		float moveSpeed = 2;

		enemyMock.Move (currentPos, target,moveSpeed, new Quaternion(), 0);

		// verify attack not called
		attackMock.DidNotReceive().StartAttacking();

		// Make enemy in range
		target = Vector3.right;
		enemyMock.Move (currentPos, target,moveSpeed, new Quaternion(), 0);
		// Verify attack called
		attackMock.Received().StartAttacking();
	}

	[Test]
	[Category("Combat")]
	public void IfEnemyMeleeAttackingStartAttackingNotCalled() {
		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();
		var enemyMock = GetEnemyMeleeControllerMock(moveMock, attackMock);

		enemyMock.attacking = true;

		enemyMock.Move (Vector3.zero, Vector3.right,2, new Quaternion(), 0);
		attackMock.DidNotReceive().StartAttacking();
	}

	[Test]
	[Category("Combat")]
	public void IfEnemyMeleeNoLongerInRangeStopAttackingCalledAndAttackingSetToFalse() {
		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();
		var enemyMock = GetEnemyMeleeControllerMock(moveMock, attackMock);
		
		enemyMock.attacking = true;
		
		enemyMock.Move (Vector3.zero, new Vector3(2,0,0),2, new Quaternion(), 0);
		attackMock.Received().StopAttacking();
	}

	[Test]
	[Category("Combat")]
	public void EnemyMeleeAttemptsHitButAttackIsNotReadySoAttackIsNotCalled() {
		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();

		var enemyMock = GetEnemyMeleeControllerMock(moveMock, attackMock);

		enemyMock.AttemptHit(5);
		attackMock.DidNotReceive().Attack();
	}

	[Test]
	[Category("Combat")]
	public void EnemyMeleeCalculateAttackCoolDownOutputsExpected() {
		var moveMock = GetMoveMock();
		var attackMock = GetAttackMock();
		
		var enemyMock = GetEnemyMeleeControllerMock(moveMock, attackMock);
	
		float actual = enemyMock.CalcAttackCoolDown(5);

		Assert.AreEqual(Time.time + 5, actual);

	}

	private IMoveController GetMoveMock () {
		return Substitute.For<IMoveController> ();
	}

	private IDestroyableController GetHitPointsMock () {
		return Substitute.For<IDestroyableController> ();
	}

	private IMeleeController GetAttackMock () {
		return Substitute.For<IMeleeController> ();
	}
	
	private EnemyMeleeController GetEnemyMeleeControllerMock (IMoveController move) {
		
		var controller = Substitute.For<EnemyMeleeController>();
		controller.SetMoveController(move);
		
		return controller;
	}

	private EnemyMeleeController GetEnemyMeleeControllerMock (IDestroyableController hp) {
		
		var controller = Substitute.For<EnemyMeleeController>();
		controller.SetHitPointsController(hp);
		
		return controller;
	}

	private EnemyMeleeController GetEnemyMeleeControllerMock (IMoveController move, IMeleeController attack) {
		
		var controller = Substitute.For<EnemyMeleeController>();
		controller.SetMoveController(move);
		controller.SetAttackController(attack);
		
		return controller;
	}
}
