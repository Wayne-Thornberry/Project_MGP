using UnityEngine;

public class TrafficLight : MonoBehaviour
{
	public int State;
	public Collider Blocker;
	

	public bool IsRed()
	{
		return State == 0;
	}

	public bool IsGreen()
	{
		return State == 1;
	}

	public void SetState(int state)
	{
		State = state;
		Blocker.enabled = State == 0;
	}
}
