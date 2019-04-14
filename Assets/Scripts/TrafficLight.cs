using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{

	public int Speed;
	public int Stage { get; set; }
	public float ElapsedTime { get; set; }
	public int Weight = 30;

	public delegate void LightChanged();
	public event LightChanged OnLightChanged;
	
	
	void Update ()
	{
	    ElapsedTime += Time.deltaTime;
		if (!(ElapsedTime > 10f)) return;
		NextStage();
		ElapsedTime = 0;
	}

	private void NextStage()
	{
		Stage++;
		if (Stage > 2) Stage = 0;
		switch (Stage)
		{
			case 0: Speed = 30; break;
			case 1: Speed = 5; break;
			case 2: Speed = 0; break;
			default: Speed = 30; break;
		}
		if (OnLightChanged != null) OnLightChanged.Invoke();
	}
}
