using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int damage = 20;
	public float speed = 8;

	public void Fire(Vector3 direction) { 
		GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x * speed , direction.y * speed,0); 
	}

	void OnTriggerEnter2D (Collider2D collider) {
		int enemyLayer = 9;

		if (collider.gameObject.layer == enemyLayer) {
			Enemy enemy = collider.gameObject.GetComponent<Enemy>();
			enemy.Hit(damage);
			Destroy (this.gameObject);
		}
	}	
}
