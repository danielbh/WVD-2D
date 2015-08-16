using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using System;

[TestFixture]
public class HitPointsTests  {

	private IHitPointsComponent component;
	private HitPointsController controller;
	
	[SetUp] public void Init() { 
		component = GetHitPointsMock();
		controller = GetControllerMockForHitPoints(component);
	}

	[Test]
	[Category("Hitpoints")]
	public void LoseHitPoints() {
		controller.ReduceHitPoints (25, 100);
		component.Received(1).ReduceHitPoints(25);
	}
	
	[Test]
	[Category("Hitpoints")]
	public void EnemyMeleeDiesOnZeroHitPoints() {
		controller.ReduceHitPoints (25,25);
		
		component.DidNotReceive().ReduceHitPoints(25);
		component.Received(1).Destroy();
	}

	public IHitPointsComponent GetHitPointsMock () {
		return Substitute.For<IHitPointsComponent> ();
	}
	
	public HitPointsController GetControllerMockForHitPoints (IHitPointsComponent component) {
		var controller = Substitute.For<HitPointsController>();
		controller.SetComponent(component);
			return controller;
	}
}
