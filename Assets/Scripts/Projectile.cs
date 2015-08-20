using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int damage = 25;
	public float speed = 8;

	public void Fire(Vector3 direction) { 
			GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x * speed , direction.y * speed,0); 
	}

	void OnTriggerEnter2D (Collider2D collider) {
		HitPoints hpComponent = collider.gameObject.GetComponent<HitPoints>();
		if (hpComponent != null) {
			hpComponent.Hit(damage);
			Destroy (this.gameObject);
		}
	}	
}
