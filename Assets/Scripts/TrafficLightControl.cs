using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System.Timers;

public class TrafficLightControl : MonoBehaviour
{
	public TrafficLight LightOne;
	public TrafficLight LightTwo;
	public TrafficLight LightThree;
	public TrafficLight LightFour;
	public bool Flip;
	public float Count;
	
	// Use this for initialization
	void Start()
	{
		LightOne.SetState(1);
		LightTwo.SetState(0);
		LightThree.SetState(0);
		LightFour.SetState(1);
	}

	void Update()
	{
		Count += Time.deltaTime;
		if (!(Count > 5.0)) return;
		Count = 0;
		LightSwitcher();
	}

	private void LightSwitcher()
	{
		Flip = !Flip;

		// Green to Red
		LightOne.SetState(Convert.ToInt32(!Flip));
		LightTwo.SetState(Convert.ToInt32(Flip));			

		// Red to Green
		LightThree.SetState(Convert.ToInt32(Flip));
		LightFour.SetState(Convert.ToInt32(!Flip));
	}
}
