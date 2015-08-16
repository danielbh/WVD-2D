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
	[Category("Movement")]
	public void MoveToPoint() {
		var moveMock = mocks.GetMoveMock();
		var enemyMock = mocks.GetEnemyMeleeControllerMock(moveMock, mocks.GetMeleeMock());
		
		Vector3 currentPos = Vector3.zero;
		Vector3 target = new Vector3(2,0,0);
		float moveSpeed = 2;
		
		enemyMock.Move (currentPos, target,moveSpeed/*, new Quaternion(), 0*/);
		
		moveMock.Received(1).Move (currentPos, target, moveSpeed * Time.deltaTime);
	}
}