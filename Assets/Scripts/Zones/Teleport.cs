using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

	public Vector3 Position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Entered");
		other.transform.position = Position;
	}
}
