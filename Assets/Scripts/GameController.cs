using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public float gameSize = 10f;
	public static ObjectPooler boidPooler;
	public GameObject boidPrefab;
	public int boidQuantity = 64;
	public static ObjectPooler finishPooler;
	public GameObject finishPrefab;
	public int finishQuantity = 1;

	private new Transform transform;
	private Vector3 position;

	// Use this for initialization
	void Start () {
		this.gameSize /= 2f;

		this.transform = gameObject.transform;
		this.position = transform.position;

		//initialise_positions()
		PoolObjects();
	}
	
	// Update is called once per frame
	void Update () {
		NewFinishes();
	}

	public void PoolObjects() {
		//draw_boids()
		PoolBoids();
		//draw_finishes()
		PoolFinishes();
	}

	public void PoolBoids() {
		boidPooler = (gameObject.AddComponent(typeof(ObjectPooler)) as ObjectPooler).SetPoolObject(boidPrefab);

		GameObject boid;
		Transform transform;
		Vector3 position;
		Quaternion rotation;
		for (int x = 0; x < boidQuantity; x++) {
			boid = boidPooler.GetPoolObject();
			transform = boid.transform;
			transform.parent = this.transform;
			position = new Vector3(
				Random.Range(this.position.x + (-gameSize), this.position.x + (gameSize))
				, Random.Range(this.position.y + (-gameSize), this.position.y + (gameSize))
				, Random.Range(this.position.z + (-gameSize), this.position.z + (gameSize))
			);
			rotation = Random.rotation;
			transform.position = position;
			transform.rotation = rotation;
			boid.SetActive(true);
		}
	}

	public void PoolFinishes() {
		finishPooler = (gameObject.AddComponent(typeof(ObjectPooler)) as ObjectPooler).SetPoolObject(finishPrefab);

		GameObject finish;
		Transform transform;
		Vector3 position;
		Quaternion rotation;
		for (int x = 0; x < finishQuantity; x++) {
			finish = finishPooler.GetPoolObject();
			transform = finish.transform;
			transform.parent = this.transform;
			position = new Vector3(
				Random.Range(this.position.x + (-gameSize), this.position.x + (gameSize))
				, Random.Range(this.position.y + (-gameSize), this.position.y + (gameSize))
				, Random.Range(this.position.z + (-gameSize), this.position.z + (gameSize))
			);
			rotation = Random.rotation;
			transform.position = position;
			transform.rotation = rotation;
			finish.SetActive(true);
		}
	}

	public void NewFinishes() {
		GameObject finish;
		Transform transform;
		Vector3 position;
		Quaternion rotation;
		for (int x = 0; x < finishPooler.objectPool.Count; x++) {
			if (Random.Range(0, 100000) < 50) {
				finish = finishPooler.objectPool[x];
				transform = finish.transform;
				position = new Vector3(
					Random.Range(this.position.x + (-gameSize), this.position.x + (gameSize))
					, Random.Range(this.position.y + (-gameSize), this.position.y + (gameSize))
					, Random.Range(this.position.z + (-gameSize), this.position.z + (gameSize))
				);
				rotation = Random.rotation;
				transform.position = position;
				transform.rotation = rotation;
			}
		}
	}
}
