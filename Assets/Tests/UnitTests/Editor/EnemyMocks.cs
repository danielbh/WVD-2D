using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

public class EnemyMocks {

	public IMoveComponent GetMoveMock () {
		return Substitute.For<IMoveComponent> ();
	}
	
	public IMeleeComponent GetMeleeMock () {
		return Substitute.For<IMeleeComponent> ();
	}

	public IAttackComponent GetAttackMock () {
		return Substitute.For<IAttackComponent> ();
	}

	public IRangedComponent GetRangedMock () {
		return Substitute.For<IRangedComponent> ();
	}
	
	public EnemyController GetEnemyMeleeComponentMock (IMoveComponent move) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveComponent(move);
		
		return controller;
	}

	
	public EnemyController GetEnemyMeleeControllerMock (IMoveComponent move, IMeleeComponent melee) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveComponent(move);
		controller.SetAttackComponent(melee);
		
		return controller;
	}

	public EnemyController GetEnemyMeleeControllerMock (IMoveComponent move, IAttackComponent attack) {
		
		var controller = Substitute.For<EnemyController>();
		controller.SetMoveComponent(move);
		controller.SetAttackComponent(attack);
		return controller;
	}
}

