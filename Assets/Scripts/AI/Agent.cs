using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

	public int MaxSpeed;
	public float Speed;
	
	
	
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2, 3))
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, 3))
			{
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, Color.yellow);
				if (hit.distance < 5)
				{
					Speed -= 5f;
				}

				if (Speed <= 0)
				{
					Speed = 0;
				}
			}
			else
			{
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, Color.white);
				Speed += 0.1f;
				if (Speed > MaxSpeed)
				{
					Speed = MaxSpeed;
				}
			}
			gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward * (Time.deltaTime * Speed);
		}
	}

	void OnDrawGizmos()
	{
		
		Color color;
		color = Color.green;
		DrawHelperAtCenter(this.transform.up, color, 5f);
		color = Color.blue;
		DrawHelperAtCenter(this.transform.forward, color, 5f);
		color = Color.red;
		DrawHelperAtCenter(this.transform.right, color, 5f);
	}

	private void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
	{
		Gizmos.color = color;
		Vector3 destination = transform.position + direction * scale;
		Gizmos.DrawLine(transform.position, destination);
	}
	
	

	public void GoTo(RoadNode node)
	{
		gameObject.transform.rotation = node.transform.rotation;
		transform.LookAt(new Vector3(node.transform.position.x, transform.position.y, node.transform.position.z));
	}
}
