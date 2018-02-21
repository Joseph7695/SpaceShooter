using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	private AudioSource audioSource;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();	
		InvokeRepeating ("Fire", delay, fireRate);
	}
	
	// Update is called once per frame
	void Fire () {
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play();
	}
}
