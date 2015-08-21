using UnityEngine;
using System.Collections;

public class TestScale : MonoBehaviour {
	
	public float targetXScale;
	public float targetYScale;
	public float threshold;

	void Update () {
		if (Vector3.Distance (new Vector3(targetXScale, targetYScale, 1), transform.localScale) < threshold) {
			IntegrationTest.Pass(gameObject);
		}
	}
}
