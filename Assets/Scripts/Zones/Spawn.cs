using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
	public Transform SpawnObject;

	void Start () {
		Instantiate(SpawnObject, gameObject.transform.position, gameObject.transform.localRotation);
	}
}
