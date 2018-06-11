using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	public List<GameObject> objectPool;
	protected GameObject poolObject;
	//private int poolSize;
	private bool extendable;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetPoolObject() {
		for(int x = 0; x < objectPool.Count; x++) {
			if(!objectPool[x].activeInHierarchy) {
				return objectPool[x];
			}
		}

		if(extendable) {
			GameObject poolObject = (GameObject) Instantiate(this.poolObject);
			objectPool.Add(poolObject);
			return poolObject;
		}

		return null;
	}

	public ObjectPooler SetPoolObject(GameObject prefab, int poolSize = 1, bool extendable = true) {
		this.poolObject = prefab;
		//this.poolSize = poolSize;
		this.extendable = extendable;

		objectPool = new List<GameObject>();
		GameObject poolObject;
		for (int x = 0; x < poolSize; x++) {
			poolObject = (GameObject) Instantiate(this.poolObject);
			poolObject.SetActive(false);
			objectPool.Add(poolObject);
		}

		return this;
	}
}
