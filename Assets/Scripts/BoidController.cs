using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour {

	public float speed = 0.001f;
	public float rotationSpeed = 5.0f;
	public float minimumSpeed = 0.001f;
	public float maximumSpeed = 2.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	public float neighbourDistance = 3.0f;
	public float avoidDistance = 2f;

	bool turning = false;

	// Use this for initialization
	void Start () {
		speed = Random.Range(minimumSpeed, maximumSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		turning = (Vector3.Distance(transform.position, new Vector3(0, (this.transform.parent.GetComponent<GameController>()).worldSize / 2f, 0)) >= (this.transform.parent.GetComponent<GameController>()).worldSize) ? true : false;
		if(turning) {
			Vector3 direction = (new Vector3(0, (this.transform.parent.GetComponent<GameController>()).worldSize / 2f, 0)) - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			speed = Random.Range(minimumSpeed, maximumSpeed);
		}
		else {
			if (Random.Range(0, 10) < 1) {
				//Invoke("Boids", 1f);
				Boids();
			}
		}
		transform.Translate(0, 0, Time.deltaTime * speed);
	}

	// FixedUpdate is called per physics step
	void FixedUpdate() {
		
	}

	void Boids() {
		List<GameObject> boids = GameController.boidPooler.objectPool;

		Vector3 vcenter = Vector3.zero;
		Vector3 vavoid = Vector3.zero;
		float gSpeed = 0.1f;

		List<GameObject> finishes = GameController.finishPooler.objectPool;
		Vector3 finishPosition = finishes[0].transform.position;

		float dist;

		int groupSize = 0;
		foreach(GameObject boid in boids) {
			if(boid != this.gameObject) {
				dist = Vector3.Distance(boid.transform.position, this.transform.position);
				if(dist <= neighbourDistance) {
					vcenter += boid.transform.position;
					groupSize++;

					if(dist < avoidDistance) {
						vavoid = vavoid + (this.transform.position - boid.transform.position);
					}

					BoidController boid2 = boid.GetComponent<BoidController>();
					gSpeed = gSpeed + boid2.speed;
				}
			}
		}

		if(groupSize > 0) {
			vcenter = vcenter / groupSize + (finishPosition - this.transform.position);
			speed = gSpeed / groupSize;

			Vector3 direction = (vcenter + vavoid) - transform.position;
			if(direction != Vector3.zero) {
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			}
		}
	}
}
