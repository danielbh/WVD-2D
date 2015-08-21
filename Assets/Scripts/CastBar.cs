//  OnTouchDown.cs
//  Allows "OnMouseDown()" events to work on the iPhone.
//  Attach to the main camera.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastBar: MonoBehaviour {

	public SpellBook spellBook;

	private const string concussiveBlast = "Concussive Blast";
	private const string frostNova = "Frost Nova";
	private const string projectileReflection = "Projectile Reflection";
	private const string summonPet = "Summon Pet";
	private const string teleport = "Teleport";
	private const string meteorShower = "Meteor Shower";
	
	void Update () {
		foreach(Touch touch in Input.touches) {
			if (touch.phase.Equals(TouchPhase.Began)) {
				Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position));
				if (collider != null ) {
					switch(collider.name) {
					case concussiveBlast: 
						AnimateSpellButton(collider.gameObject);
						spellBook.CastConcussiveBlast();
						break;
					case frostNova: 
						Debug.Log (frostNova);
						break;
					case projectileReflection: 
						Debug.Log (projectileReflection);
						break;
					case summonPet: 
						Debug.Log (summonPet);
						break;
					case teleport: 
						Debug.Log(teleport);
						break;
					case meteorShower: 
						Debug.Log (meteorShower);
						break;
					default: 
						Debug.LogError("Invalid cast bar touch");
						break;
					}
				}
			}
		}
	}

	private void AnimateSpellButton (GameObject spell) {
		// TODO: cast animation of button
	}
}
