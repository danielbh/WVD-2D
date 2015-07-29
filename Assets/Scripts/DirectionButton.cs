using UnityEngine;
using System.Collections;

public class DirectionButton : MonoBehaviour {

	private Vector3 direction;
	
	void Start () {
		switch(name) {
		case "Up": direction = Vector3.up; break;
		case "UpLeft": direction = new Vector3(- 1, 1, 0); break;
		case "Left": direction = Vector3.left; break;
		case "DownLeft": direction =  new Vector3(- 1, -1, 0); break;
		case "Down": direction = Vector3.down; break;
		case "DownRight": direction =  new Vector3(1, -1, 0); break;
		case "Right": direction = Vector3.right; break;
		case "UpRight": direction =  new Vector3(1, 1, 0); break;
		default: Debug.Log ("Invalid value for DirectionButton name"); break;
		}
	}
	
	public void MovePlayerInDirection () {
		GameObject.Find("Player").GetComponent<PlayerController>().Move (direction);
	}
}
