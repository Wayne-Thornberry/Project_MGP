using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

	public Text Log;
	
	// Use this for initialization
	void Start ()
	{
		Log.text = SpawnController.Log;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
