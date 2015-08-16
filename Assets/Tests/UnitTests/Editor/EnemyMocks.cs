using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

public class EnemyMocks {

	public IMoveComponent GetMoveMock () {
		return Substitute.For<IMoveComponent> ();
	}
	
	public IMeleeComponent GetAttackMock () {
		return Substitute.For<IMeleeComponent> ();
	}
	
	public EnemyController GetEnemyMeleeComponentMock (IMoveComponent move) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveComponent(move);
		
		return controller;
	}

	
	public EnemyController GetEnemyMeleeControllerMock (IMoveComponent move, IMeleeComponent attack) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveComponent(move);
		controller.SetAttackComponent(attack);
		
		return controller;
	}
}

