using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public AudioClip friedSound;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	public float maxHeat;
	private bool overheated;
	private int heat;
	private int overheatingTimer = 200;

	void Start() {
		overheated = false;
	}

	void Update() {
		if (Input.GetButton ("Fire1") && Time.time > nextFire && !overheated) {
			nextFire = Time.time + fireRate; 
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//missileSound.Play ();
			audio.Play ();
			heat += 150; // Guns burnin' up!
			if (heat > maxHeat) {
				AudioSource.PlayClipAtPoint(friedSound, Camera.main.transform.position);
				overheated = true; // Oops, got carried away, guns fried
			}
		}
		// If the user was NOT able to fire, cool down the guns
		heat-=10;
		if (heat < 0)
			heat = 0;
		Debug.Log (heat);
		if (overheated) {
			Debug.Log ("Overheated!");
			--overheatingTimer;
			if (overheatingTimer < 1) {
				heat = 0;
				overheated = false;
				overheatingTimer = 600;
			}
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3 (
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}