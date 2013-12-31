using UnityEngine;
using System.Collections;

public class MoverRand : MonoBehaviour {

	public float speed;
	void Start() {
		// Randomize speed of object between bounds
		float lower = 0.3f * speed;
		float upper = 2.0f * speed;
		rigidbody.velocity  = transform.forward * Random.Range (lower, upper);
	}
}