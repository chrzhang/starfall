using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait; // Resting period at beginning
	public float waveWait;

	private int difficulty;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;

	private int score;

	void Start() {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine (SpawnWaves());
		difficulty = 1;
	}

	void Update() {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves() {
		float randomno;
		Vector3 scale;
		yield return new WaitForSeconds(startWait);
		while(true) {
			for (int i = 0; i < (int)(hazardCount * 0.3); ++i) {	
				for (int j = 0; j < difficulty; ++j) {
					// Randomly pick and scale the asteroid
					GameObject hazard = hazards [Random.Range (0, hazards.Length)];
					randomno = Random.Range (0.2f, 1.3f);
					scale.x = randomno;
					randomno = Random.Range (0.2f, 1.3f);
					scale.y = randomno;
					randomno = Random.Range (0.2f, 1.3f);
					scale.z = randomno;
					hazard.transform.localScale = scale;
					// Randomly choose where to spawn it
					Vector3 spawnPosition = new Vector3(Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate(hazard, spawnPosition, spawnRotation);

				}
				yield return new WaitForSeconds(spawnWait);
			}
			yield return  new WaitForSeconds(waveWait);
			if (gameOver) {
				restartText.text = "Press 'R' for restart";
				restart = true;
				break;
			}
			if (difficulty < 3)
				++difficulty; // Increase with each round until 3, then reset
			else
				difficulty = 1;
		}
	}

	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}