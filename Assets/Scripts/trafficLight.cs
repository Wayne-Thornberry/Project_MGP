using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class trafficLight : MonoBehaviour 
{
	public int State { get; private set; }
	Collider traffic_Collider;

	public trafficLight()
	{
		SetState(State);
	}

	public bool IsGreen()
	{
		return State == 1;
	}

	public bool IsOrange()
	{
		return State == 2;
		
	}

	public bool isRed()
	{
		return State == 3;
	}

	public void SetState(int state)
	{
		if (state == 1)
		{
			State = state;
			traffic_Collider.enabled = !traffic_Collider.enabled;
		}

		else if(state == 2)
		{
			State = state;	
			traffic_Collider.enabled = !traffic_Collider.enabled;
		}
		else if (state == 3)
		{
			State = state;
			traffic_Collider.enabled = !traffic_Collider.enabled;
		}
		else
		{
			State = state;
			traffic_Collider.enabled = !traffic_Collider.enabled;
		}
	}
}
