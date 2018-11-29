using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System.Timers;

public class TrafficLightControl : trafficLight
{
	public static trafficLight lightOne;
	public static trafficLight lightTwo;
	public static trafficLight lightThree;
	public static trafficLight lightFour;


	public static Timer countDown = new Timer(50000);
	
	// Use this for initialization
	void Start()
	{
		lightOne.SetState(1);
		lightTwo.SetState(3);
		lightThree.SetState(1);
		lightFour.SetState(3);
		
		SetTimer();
	}

	private static void SetTimer()
	{
		// event handler for when the timer reaches 5 sec
		countDown.Elapsed += LightSwitcher;
		countDown.AutoReset = true;
		countDown.Enabled = true;

	}

	private static void LightSwitcher(object source,ElapsedEventArgs e)
	{
		if(lightOne.IsGreen() && lightThree.IsGreen())
		{
			// Green to Red
			lightOne.SetState(3);
			lightThree.SetState(3);			

			// Red to Green
			lightTwo.SetState(1);
			lightTwo.SetState(1);
		}
		else if (lightOne.isRed() && lightThree.isRed())
		{
			// Green to Red
			lightTwo.SetState(3);
			lightTwo.SetState(3);
			
			// Red to Green
			lightOne.SetState(1);
			lightThree.SetState(1);			
		}
	}
	
	void Update () {
		
	}
}
