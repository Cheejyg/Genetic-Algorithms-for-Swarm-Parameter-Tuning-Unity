using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour {

	public float speed = 0.5f;

	// Use this for initialization
	void Start () {
		speed = Random.Range(0, speed);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0, Time.deltaTime * speed);
	}

	// FixedUpdate is called per physics step
	void FixedUpdate() {
		
	}
}
