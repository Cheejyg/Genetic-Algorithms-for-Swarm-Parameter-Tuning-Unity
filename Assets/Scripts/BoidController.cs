using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour {

	public float boidSize = 0.10922f; //0.10922f (0.10922m, 4.3in)
	public float speed = 0.001f; //0.001f (1mm/s)
	public float rotationSpeed = 1f; //1f, 15f, 30f
	public float maximumSpeed = 4/9f; //4/9f (4r9 m/s), 10/81f (10r81 m/s)
	/*Vector3 averageHeading;
	Vector3 averagePosition;*/
	public float neighbourDistance = 8f, sqrNeighbourDistance; //4f (4m), 8f (8m)
	public float avoidDistance = 1.5f, sqrAvoidDistance; //1.25f (125%), 1.5f (150%)

	bool turning = false;

	private new Rigidbody rigidbody;
	private new Transform transform;
	private Vector3 position;
	private Quaternion rotation;

	// Use this for initialization
	void Start () {
		this.speed = Random.Range(speed, maximumSpeed);
		this.speed /= Time.deltaTime;
		
		this.neighbourDistance += boidSize;
		this.sqrNeighbourDistance = neighbourDistance * neighbourDistance;
		this.avoidDistance = (avoidDistance * boidSize) + boidSize;
		this.sqrAvoidDistance = avoidDistance * avoidDistance;
		
		this.rigidbody = GetComponent<Rigidbody>();
		this.transform = gameObject.transform;
		this.position = transform.position;
		this.rotation = transform.rotation;

		rigidbody.velocity = transform.forward * Time.deltaTime * speed;
	}

	// Update is called once per frame
	void Update () {

	}

	// FixedUpdate is called per physics step
	void FixedUpdate() {
		Boids();
	}

	//move_all_boids_to_new_positions()
	void Boids() {
		position = transform.position;
		rotation = transform.rotation;

		Vector3 cohesion, seperation, alignment;
		Vector3 finish;

		seperation = Separation(this.gameObject);
		alignment = Alignment(this.gameObject);
		cohesion = Cohesion(this.gameObject);
		finish = Finish(this.gameObject);

		Vector3 target = seperation + alignment + cohesion + finish;
		Vector3 targetPosition = position + target;

		/*Debug.DrawLine(position, position + seperation, Color.red);
		Debug.DrawLine(position, position + alignment, Color.green);
		Debug.DrawLine(position, position + cohesion, Color.blue);
		Debug.DrawLine(position, position + finish, Color.cyan);

		Debug.DrawLine(position, targetPosition, Color.white);*/

		/*transform.position += target * Time.deltaTime * speed;
		transform.Translate(target * Time.deltaTime);
		rigidbody.AddForce(target, ForceMode.Force);
		rigidbody.MovePosition(targetPosition * Time.deltaTime);
		rigidbody.velocity = target * Time.deltaTime;*/

		if (target != Vector3.zero) {
			transform.rotation = Quaternion.Slerp(rotation, Quaternion.LookRotation(targetPosition - position, transform.up), Time.deltaTime * rotationSpeed);
			//transform.rotation = Quaternion.LookRotation(targetPosition - position, transform.up);
		}

		rigidbody.velocity = transform.forward * Time.deltaTime * speed;
	}

	//Separation: steer to avoid crowding local flockmates
	private Vector3 Separation(GameObject bJ) {
		position = bJ.transform.position;
		Vector3 boidPosition, c = Vector3.zero;

		foreach (GameObject boid in GameController.boidPooler.objectPool) {
			if (boid != bJ) {
				boidPosition = boid.transform.position;
				if (/*Vector3.Distance(boidPosition, position) <= avoidDistance*/ (boidPosition - position).sqrMagnitude <= sqrAvoidDistance) {
					c -= boidPosition - position;
				}
			}
		}

		return c;
	}

	//Alignment: steer towards the average heading of local flockmates
	private Vector3 Alignment(GameObject bJ) {
		position = bJ.transform.position;
		Vector3 pvJ = Vector3.zero;
		int neighbours = 0;

		foreach (GameObject boid in GameController.boidPooler.objectPool) {
			if (boid != bJ) {
				if (/*Vector3.Distance(boid.transform.position, position) <= neighbourDistance*/ (boid.transform.position - position).sqrMagnitude <= sqrNeighbourDistance / 2f) {
					pvJ += boid.GetComponent<Rigidbody>().velocity;
					neighbours++;
				}
			}
		}

		return neighbours < 1 ? pvJ : (pvJ / (float)neighbours);
	}

	//Cohesion: steer to move toward the average position of local flockmates
	private Vector3 Cohesion(GameObject bJ) {
		position = bJ.transform.position;
		Vector3 boidPosition, pcJ = Vector3.zero;
		int neighbours = 0;

		foreach (GameObject boid in GameController.boidPooler.objectPool) {
			if (boid != bJ) {
				boidPosition = boid.transform.position;
				if (/*Vector3.Distance(boidPosition, position) <= neighbourDistance*/ (boidPosition - position).sqrMagnitude <= sqrNeighbourDistance) {
					pcJ += boidPosition;
					neighbours++;
				}
			}
		}

		return neighbours < 1 ? pcJ : (pcJ / (float)neighbours) - position;
	}

	//Finish: 
	private Vector3 Finish(GameObject bJ) {
		position = bJ.transform.position;
		Vector3 finishPosition;

		foreach (GameObject finish in GameController.finishPooler.objectPool) {
			finishPosition = finish.transform.position;
			if (/*Vector3.Distance(finishPosition, position) <= neighbourDistance*/ (finishPosition - position).sqrMagnitude <= sqrNeighbourDistance) {
				return finishPosition - position;
			}
		}

		return Vector3.zero;
	}
}
