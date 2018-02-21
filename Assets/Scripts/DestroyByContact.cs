using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public int scoreValue;
	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController> ();
		if (gameController == null)
			Debug.Log("Cannot find 'GameController' script");
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
			return;
		if (other.CompareTag("Player")) {	
			Instantiate (playerExplosion, other.GetComponent<Transform> ().position, other.GetComponent<Transform> ().rotation);
			gameController.GameOver ();
		}

		Destroy (other.gameObject);
		Destroy (gameObject);

		gameController.AddScore (scoreValue);
		if (explosion != null)
			Instantiate (explosion, GetComponent<Transform> ().position, GetComponent<Transform> ().rotation);
	}
}
