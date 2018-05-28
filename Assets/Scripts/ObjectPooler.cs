using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	//public static ObjectPooler objectPooler;
	public List<GameObject> objectPool;
	protected GameObject poolObject;
	private int maximumPool;
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
			GameObject poolObject = (GameObject)Instantiate(this.poolObject);
			objectPool.Add(poolObject);
			return poolObject;
		}

		return null;
	}

	public void SetPoolObject(GameObject prefab, int maximumPool = 2, bool extendable = true) {
		this.poolObject = prefab;
		this.maximumPool = 2;
		this.extendable = extendable;

		objectPool = new List<GameObject>();
		for (int x = 0; x < this.maximumPool; x++) {
			GameObject poolObject = (GameObject)Instantiate(this.poolObject);
			poolObject.SetActive(false);
			objectPool.Add(poolObject);
		}
	}
}
