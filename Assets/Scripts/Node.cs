using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Node : MonoBehaviour
{
	public List<Node> Nodes;
	public string Name;
	public int Weight;

	public Node()
	{
		Nodes = new List<Node>();
	}

	public void ConnectNode(Node node)
	{
		Nodes.Add(node);
	}

	private void OnDrawGizmos()
	{
		foreach (var connection in Nodes)
		{
			if(connection != null)
				Debug.DrawLine(transform.position, connection.transform.position, Color.green);
		}
	}

	public int GetWeight(Node destination)
	{
		return (int) Vector3.Distance(this.transform.position, destination.transform.position);
	}

}
