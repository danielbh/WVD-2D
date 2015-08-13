using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int damage = 20;

	void OnTriggerEnter2D (Collider2D collider) {
		int enemyLayer = 9;

		if (collider.gameObject.layer == enemyLayer) {
			Enemy enemy = collider.gameObject.GetComponent<Enemy>();
			enemy.Hit(damage);
			Destroy (this.gameObject);
		}
	}	
}
