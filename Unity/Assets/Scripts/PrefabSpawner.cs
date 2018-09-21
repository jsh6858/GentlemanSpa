using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

	public GameObject _objPrefab;
	void Awake()
	{
		GameObject obj = GameObject.Instantiate(_objPrefab, transform.position, transform.rotation);

		if(transform.parent != null)
			obj.transform.SetParent(transform.parent);

		Destroy(gameObject);
	}
}
