using UnityEngine;
using System.Collections;

public class CallProjectileFunction : MonoBehaviour {

	public Vector3 direction;

	// Use this for initialization
	void Start () {
		GetComponent<Projectile>().Fire(direction);
	}
}
