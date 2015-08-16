using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

[TestFixture]
public class EnemyMeleeTests  {

	private EnemyMocks mocks;

	[SetUp] public void Init() { 
		mocks = new EnemyMocks();
	}

	[Test]
	[Category("Movement")]
	public void MoveToPoint() {
		var moveMock = mocks.GetMoveMock();
		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, mocks.GetMeleeMock());

		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(2,0,0);
		float moveSpeed = 2;

		enemyMock.Move (currentPos, target,moveSpeed);
		
		moveMock.Received(1).Move (currentPos, target, moveSpeed * Time.deltaTime);
	}

	[Test]
	[Category("Movement")]
	public void EnemyMeleeNeverGoesUnderWizard() {
		var moveMock = mocks.GetMoveMock();
		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, mocks.GetAttackMock());

		Vector3 currentPos = Vector3.zero;
		Vector3 target = Vector3.right;
		float moveSpeed = 2;
		
		Vector3 expected = enemyMock.Move (currentPos, target,moveSpeed, 1);

		Assert.AreEqual(expected, currentPos);
	}
	


	[Test]
	[Category("Combat")]
	public void EnemyMeleeAttacksWhenWithinRange() {

		var moveMock = mocks.GetMoveMock();
		var attackMock = mocks.GetAttackMock();
		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, attackMock);

		// EnemyMelee not within range
		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(2,0,0);

		enemyMock.HandleAttacks (currentPos, target, 1);

		// verify attack not called
		attackMock.DidNotReceive().StartAttacking();

		// Make enemy in range
		target = Vector3.right;
		enemyMock.HandleAttacks (currentPos, target, 1);
		// Verify attack called
		attackMock.Received().StartAttacking();
	}

	[Test]
	[Category("Combat")]
	public void IfEnemyMeleeAttackingStartAttackingNotCalled() {
		var moveMock = mocks.GetMoveMock();
		var attackMock = mocks.GetMeleeMock();
		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, attackMock);

		enemyMock.attacking = true;

		enemyMock.HandleAttacks (Vector3.zero, Vector3.right,1);
		attackMock.DidNotReceive().StartAttacking();
	}

	[Test]
	[Category("Combat")]
	public void IfEnemyMeleeNoLongerInRangeStopAttackingCalledAndAttackingSetToFalse() {
		var moveMock = mocks.GetMoveMock();
		var attackMock = mocks.GetAttackMock();
		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, attackMock);
		
		enemyMock.attacking = true;
		
		enemyMock.HandleAttacks (Vector3.zero, new Vector3(2,0,0),1);
		attackMock.Received().StopAttacking();
	}

	[Test]
	[Category("Combat")]
	public void EnemyMeleeAttemptsHitButAttackIsNotReadySoAttackIsNotCalled() {
		var moveMock = mocks.GetMoveMock();
		var attackMock = mocks.GetMeleeMock();

		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, attackMock);

		enemyMock.IsAttackReady().Returns(false);

		enemyMock.AttemptHit(5);
		attackMock.DidNotReceive().Attack();
	}

	[Test]
	[Category("Combat")]
	public void EnemyMeleeAttemptsHitAndAttackIsReadySoAttackIsCalled() {
		var moveMock = mocks.GetMoveMock();
		var attackMock = mocks.GetMeleeMock();
		
		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, attackMock);

		enemyMock.IsAttackReady().Returns(true);
		enemyMock.AttemptHit(5);
		attackMock.Received().Attack();
	}

	[Test]
	[Category("Combat")]
	public void EnemyMeleeCalculateAttackCoolDownOutputsExpected() {
		var moveMock = mocks.GetMoveMock();
		var attackMock = mocks.GetMeleeMock();
		
		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, attackMock);
	
		float actual = enemyMock.CalcAttackCoolDown(5);

		Assert.AreEqual(Time.time + 5, actual);
	}	
}
