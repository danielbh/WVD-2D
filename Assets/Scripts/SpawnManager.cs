using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public GameObject demonMelee;
	public GameObject demonRanged;
	public int wave = 1;

	public GameObject[] spawners;

	void Start () {
		spawners = GameObject.FindGameObjectsWithTag("Spawner");
	}


	void Update() {

	GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if (enemies.Length == 0 && wave != 4) {
			Invoke ("Wave" + wave, 0);
			Debug.Log ("Wave " + wave + " called!");
			++wave;
		}
	}

	public void Wave1() {
		Instantiate(demonMelee, spawners[0].transform.position, Quaternion.identity);
		Instantiate(demonMelee, spawners[2].transform.position, Quaternion.identity);
		Instantiate(demonMelee, spawners[4].transform.position, Quaternion.identity);
	}

	public void Wave2() {
		Instantiate(demonMelee, spawners[0].transform.position, Quaternion.identity);
		Instantiate(demonMelee, spawners[2].transform.position, Quaternion.identity);
		Instantiate(demonMelee, spawners[4].transform.position, Quaternion.identity);

		StartCoroutine(SpawnNextSet (3, new GameObject[] {demonMelee, demonMelee, demonMelee}, new int[] {1,3,5}));
	}

	public void Wave3() {
		Instantiate(demonMelee, spawners[0].transform.position, Quaternion.identity);
		Instantiate(demonRanged, spawners[2].transform.position, Quaternion.identity);
		Instantiate(demonMelee, spawners[4].transform.position, Quaternion.identity);

		StartCoroutine(SpawnNextSet (3,new GameObject[] {demonMelee, demonRanged, demonRanged}, new int[] {1,3,5}));
	}

	public IEnumerator SpawnNextSet(float seconds, GameObject[] demons, int[] spawnerIndexes) {
		yield return new WaitForSeconds(seconds);

		int i= 0;

		foreach(GameObject demon in demons) {
			Instantiate(demon, spawners[spawnerIndexes[i]].transform.position, Quaternion.identity);
			++i;
		}

	}
}
