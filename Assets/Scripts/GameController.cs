using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public float worldSize = 20f;
	public GameObject boidPrefab;
	public int boidQuantity = 64;

	// Use this for initialization
	void Start () {
		//ObjectPooler objectPooler = GetComponent<ObjectPooler>();
		ObjectPooler boidPooler = gameObject.AddComponent<ObjectPooler>() as ObjectPooler;
		boidPooler.SetPoolObject(boidPrefab);

		for(int x = 0; x < boidQuantity; x++) {
			GameObject boid = boidPooler.GetPoolObject();
			Vector3 position = new Vector3(
				Random.Range(-worldSize/2, worldSize/2)
				, Random.Range(boidPrefab.transform.position.y, worldSize)
				, Random.Range(-worldSize/2, worldSize/2)
			);
			boid.transform.position = position;
			boid.transform.rotation = Quaternion.LookRotation(position, position);
			boid.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
