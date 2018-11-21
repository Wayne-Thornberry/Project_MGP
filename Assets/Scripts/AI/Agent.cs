using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

	public int MaxSpeed;
	public int Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20, 3))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, Color.yellow);
			Speed--;
			if (Speed <= 0)
			{
				Speed = 0;
			}
		}
		else
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, Color.white);
			Speed++;
			if (Speed > MaxSpeed)
			{
				Speed = MaxSpeed;
			}
		}
		gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward * (Time.deltaTime * Speed);
	}

	void OnDrawGizmos()
	{
		
		Color color;
		color = Color.green;
		// local up
		DrawHelperAtCenter(this.transform.up, color, 2f);
         
		color.g -= 0.5f;
		// global up
		DrawHelperAtCenter(Vector3.up, color, 1f);
         
		color = Color.blue;
		// local forward
		DrawHelperAtCenter(this.transform.forward, color, 2f);
         
		color.b -= 0.5f;
		// global forward
		DrawHelperAtCenter(Vector3.forward, color, 1f);
         
		color = Color.red;
		// local right
		DrawHelperAtCenter(this.transform.right, color, 2f);
         
		color.r -= 0.5f;
		// global right
		DrawHelperAtCenter(Vector3.right, color, 1f);
		// Does the ray intersect any objects excluding the player layer
	}

	private void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
	{
		Gizmos.color = color;
		Vector3 destination = transform.position + direction * scale;
		Gizmos.DrawLine(transform.position, destination);
	}
}
