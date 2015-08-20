using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

[TestFixture]
public class EnemyRangedTests  {
	
	private EnemyMocks mocks;

	[SetUp] public void Init() { 
		mocks = new EnemyMocks();
	}

	[Test]
	[Category("Combat")]
	public void EnemyDoesNotAttackWhenNotInRange(){
		var attackMock = mocks.GetRangedMock();
		var enemyMock = mocks.GetEnemyRangedControllerMock(mocks.GetMoveMock(), attackMock);

		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(11,0,0);

		enemyMock.HandleAttacks(currentPos, target, 10);

		enemyMock.attackComponent.DidNotReceive().StartAttacking();
	}
	
	[Test]
	[Category("Combat")]
	public void EnemyAttacksWhenInRange(){
		var attackMock = mocks.GetRangedMock();
		var enemyMock = mocks.GetEnemyRangedControllerMock(mocks.GetMoveMock(), attackMock);
		
		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(10,0,0);

		Assert.IsNotNull(enemyMock.rangedAttackComponent);
		
		enemyMock.HandleAttacks(currentPos, target, 10);
		
		enemyMock.attackComponent.Received(1).StartAttacking();
	}

	[Test]
	[Category("Combat")]
	public void AttemptHitReceivesAimVectorFromAimFunction(){
		var attackMock = mocks.GetRangedMock();
		var enemyMock = mocks.GetEnemyRangedControllerMock(mocks.GetMoveMock(), attackMock);

		enemyMock.Aim().Returns(Vector3.right);

		enemyMock.IsAttackReady().Returns(true);
		enemyMock.AttemptHit(5);
		
		attackMock.Received(1).Attack(Vector3.right);
	}

	[Test]
	[Category("Movement")]
	public void EnemyStaysBackAtCertainDistance() {
		var moveMock = mocks.GetMoveMock();
		var enemyMock = mocks.GetEnemyRangedControllerMock(moveMock, mocks.GetRangedMock());
		
		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(4,0,0);
		float moveSpeed = 2;
		
		enemyMock.Move(currentPos, target,moveSpeed, 5);
		
		moveMock.DidNotReceive().Move (currentPos, target, moveSpeed * Time.deltaTime);
	}
}


