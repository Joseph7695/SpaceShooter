using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text restartText;
	public Text gameOverText;
	public Text scoreText;

	private bool gameOver;
	private bool restart;
	private int score;
	private float hazardArraySize;

	void Start(){
		score = 0;
		UpdateScore();
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		hazardArraySize = hazards.Length;
		StartCoroutine(	SpawnWaves ());
	}

	void Update(){
		if (restart) {
			if (Input.GetKey(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		while (true){
			for (int i = 0 ; i<hazardCount ; i++){
				GameObject hazard = hazards[(int)Random.Range(0,(float)hazardArraySize)];
				Vector3 spawnPosition = new Vector3 (Random.Range(-6,6),0,16);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver(){
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}
}
