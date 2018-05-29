using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public float worldSize = 20f;
	public static ObjectPooler finishPooler;
	public GameObject finishPrefab;
	public int finishQuantity = 1;
	public static ObjectPooler boidPooler;
	public GameObject boidPrefab;
	public int boidQuantity = 128;

	// Use this for initialization
	void Start () {
		PoolObjects();
	}
	
	// Update is called once per frame
	void Update () {
		NewFinishes();
	}

	public void PoolObjects() {
		PoolBoids();
		PoolFinishes();
	}

	public void PoolBoids() {
		boidPooler = gameObject.AddComponent<ObjectPooler>() as ObjectPooler;
		boidPooler.SetPoolObject(boidPrefab);
		
		for (int x = 0; x < boidQuantity; x++) {
			GameObject boid = boidPooler.GetPoolObject();
			boid.transform.parent = this.transform;
			Vector3 position = new Vector3(
				Random.Range(-worldSize / 2, worldSize / 2)
				, Random.Range(boidPrefab.transform.position.y, worldSize)
				, Random.Range(-worldSize / 2, worldSize / 2)
			);
			boid.transform.position = position;
			boid.transform.rotation = Quaternion.LookRotation(position, position);
			boid.SetActive(true);
		}
	}

	public void PoolFinishes() {
		finishPooler = gameObject.AddComponent<ObjectPooler>() as ObjectPooler;
		finishPooler.SetPoolObject(finishPrefab);

		for (int x = 0; x < finishQuantity; x++) {
			GameObject finish = finishPooler.GetPoolObject();
			finish.transform.parent = this.transform;
			Vector3 position = new Vector3(
				Random.Range(-worldSize / 2, worldSize / 2)
				, Random.Range(finishPrefab.transform.position.y, worldSize)
				, Random.Range(-worldSize / 2, worldSize / 2)
			);
			finish.transform.position = position;
			finish.transform.rotation = Quaternion.LookRotation(position, position);
			finish.SetActive(true);
		}
	}

	public void NewFinishes() {
		foreach (GameObject finish in finishPooler.objectPool) {
			if (Random.Range(0, 100000) < 50) {
				finish.transform.position = new Vector3(
					Random.Range(-worldSize / 2, worldSize / 2)
					, Random.Range(finishPrefab.transform.position.y, worldSize)
					, Random.Range(-worldSize / 2, worldSize / 2)
				);
			}
		}
	}
}
