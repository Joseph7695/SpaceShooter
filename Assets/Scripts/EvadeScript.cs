using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeScript : MonoBehaviour {

	public float dodge;
	public float smoothing;
	public float tilt;
	public Boundary boundary;

	public Vector2 startWait;
	public Vector2 manuverTime;
	public Vector2 manuverWait;

	private Transform playerTransform;
	private float targetManuver;
	private Rigidbody rb;
	private float currentSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade(){
		yield return new WaitForSeconds(Random.Range (startWait.x,startWait.y));

		while (true) {
			targetManuver = playerTransform.position.x;
			yield return new WaitForSeconds (Random.Range(manuverTime.x, manuverTime.y));
			targetManuver = 0;
			yield return new WaitForSeconds (Random.Range(manuverWait.x, manuverWait.y));
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		float newManuver = Mathf.MoveTowards (rb.velocity.x, targetManuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManuver, 0.0f, currentSpeed);
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
