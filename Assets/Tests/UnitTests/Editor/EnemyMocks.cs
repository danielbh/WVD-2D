using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

public class EnemyMocks {

	public IMoveController GetMoveMock () {
		return Substitute.For<IMoveController> ();
	}
	
	public IDestroyableController GetHitPointsMock () {
		return Substitute.For<IDestroyableController> ();
	}
	
	public IMeleeController GetAttackMock () {
		return Substitute.For<IMeleeController> ();
	}
	
	public EnemyController GetEnemyMeleeControllerMock (IMoveController move) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveController(move);
		
		return controller;
	}
	
	public EnemyController GetEnemyMeleeControllerMock (IDestroyableController hp) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetHitPointsController(hp);
		
		return controller;
	}
	
	public EnemyController GetEnemyMeleeControllerMock (IMoveController move, IMeleeController attack) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveController(move);
		controller.SetAttackController(attack);
		
		return controller;
	}
}

