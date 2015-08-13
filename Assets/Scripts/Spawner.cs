using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject demon1;
	public GameObject enemiesParent;

	public float spawnRate = 2;
	// Use this for initialization
	void Start () {
		StartCoroutine("SpawnSequence");
	}

	public  IEnumerator SpawnSequence() {
		for (;;) {
			Spawn();
			yield return new WaitForSeconds(spawnRate);
		}
	}

	public void Spawn() {
		GameObject sprite = Instantiate(demon1, transform.position, Quaternion.identity) as GameObject; 
	}
}
