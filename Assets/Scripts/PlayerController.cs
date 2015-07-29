using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	
	public float projectileSpeed;
	public GameObject projectile;
	public float movementSpeed= 1 ;
	
	public void Fire () {
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject beam = Instantiate(projectile, transform.position+offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed * Time.deltaTime, 0);
	}
	
	void FaceDirection () {
		
	}
	
	public void Move(Vector3 posChange) {
		transform.position += posChange * movementSpeed * Time.deltaTime;
	}
}
