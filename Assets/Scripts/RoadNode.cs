using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{

	public RoadNode LinkedNode;

	void OnDrawGizmos()
	{
		
		Color color;
		color = Color.green;
		DrawHelperAtCenter(this.transform.up, color, 2f);
		color = Color.blue;
		DrawHelperAtCenter(this.transform.forward, color, 2f);
		color = Color.red;
		DrawHelperAtCenter(this.transform.right, color, 2f);
	}

	
	private void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
	{
		Gizmos.color = color;
		Vector3 destination = transform.position + direction * scale;
		Gizmos.DrawLine(transform.position, destination);
	}

	
	private void OnTriggerEnter(Collider other)
	{
		var test = other.GetComponent<Agent>();
		test.GoTo(LinkedNode);
		Debug.Log(test.Speed);
	}
}
