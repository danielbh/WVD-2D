using UnityEngine;
using System.Collections;

public class Despawner : MonoBehaviour {

	public float secondsUntilDespawn = 0.5f;
	
	void Start () {
		Invoke ("Despawn", secondsUntilDespawn );
	}

	void Despawn() {
		Destroy (gameObject);
	}
}
