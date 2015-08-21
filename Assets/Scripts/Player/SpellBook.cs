using UnityEngine;
using System.Collections;

public class SpellBook : MonoBehaviour {

	public GameObject concussiveBlast;
	private GameObject aoeSpellEffect;

	void Update() {
		if (aoeSpellEffect != null) {
			aoeSpellEffect.transform.position = transform.position;
		}
	}
	
	public void CastConcussiveBlast() {
		aoeSpellEffect = Instantiate(concussiveBlast, transform.position, Quaternion.identity) as GameObject;
	}
}
