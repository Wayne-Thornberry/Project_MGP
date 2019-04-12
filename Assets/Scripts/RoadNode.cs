using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
	public List<RoadNode> Nodes;
	public string Name;
	public int Weight;
	public TrafficLight TrafficLight;

	public RoadNode()
	{
		Nodes = new List<RoadNode>();
	}

	public void ConnectNode(RoadNode roadNode)
	{
		Nodes.Add(roadNode);
	}

	private void OnDrawGizmos()
	{
		foreach (var connection in Nodes)
		{
			if(connection != null)
				Debug.DrawLine(transform.position, connection.transform.position, Color.green);
		}
	}

	public int GetWeight(RoadNode destination)
	{
		if(TrafficLight != null)
		return (int) Vector3.Distance(this.transform.position, destination.transform.position) + TrafficLight.Weight;
		return (int) Vector3.Distance(this.transform.position, destination.transform.position);
	}

}
