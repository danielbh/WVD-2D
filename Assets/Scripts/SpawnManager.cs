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
		SpawnDemons(new GameObject[] {demonMelee, demonMelee, demonMelee}, new int[] {0,2,4});
	}
	
	public void Wave2() {
		SpawnDemons(new GameObject[] {demonMelee, demonMelee, demonMelee}, new int[] {0,2,4});
		StartCoroutine(SpawnNextSet (3, new GameObject[] {demonMelee, demonMelee, demonMelee}, new int[] {1,3,5}));
	}
	
	public void Wave3() {
		SpawnDemons(new GameObject[] {demonMelee, demonRanged, demonMelee}, new int[] {0,2,4});
		StartCoroutine(SpawnNextSet (3,new GameObject[] {demonMelee, demonRanged, demonRanged}, new int[] {1,3,5}));
	}
	
	
	public void SpawnDemons(GameObject[] demons, int[] spawnerIndexes) {
		int i= 0;
		
		foreach(GameObject demon in demons) {
			Instantiate(demon, spawners[spawnerIndexes[i]].transform.position, Quaternion.identity);
			++i;
		}
	}
	
	public IEnumerator SpawnNextSet(float seconds, GameObject[] demons, int[] spawnerIndexes) {
		yield return new WaitForSeconds(seconds);
		SpawnDemons(demons, spawnerIndexes);
	}
}
