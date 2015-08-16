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
		float moveSpeed = 2;
		
		enemyMock.MoveAsRanged(currentPos, target,moveSpeed);

		enemyMock.attackComponent.DidNotReceive().StartAttacking();
	}
	
	[Test]
	[Category("Combat")]
	public void EnemyAttacksWhenInRange(){
		var attackMock = mocks.GetRangedMock();
		var enemyMock = mocks.GetEnemyRangedControllerMock(mocks.GetMoveMock(), attackMock);
		
		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(10,0,0);
		float moveSpeed = 2;

		Assert.IsNotNull(enemyMock.rangedAttackComponent);
		
		enemyMock.MoveAsRanged (currentPos, target,moveSpeed);
		
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
		Vector3 target = new Vector3(5,0,0);
		float moveSpeed = 2;
		
		enemyMock.MoveAsRanged(currentPos, target,moveSpeed);
		
		moveMock.DidNotReceive().Move (currentPos, target, moveSpeed * Time.deltaTime);
	}
}