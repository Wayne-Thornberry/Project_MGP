using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionsController : MonoBehaviour {

	public static RoadNode[] RoadNodes { get; set; }
	// Use this for initialization
	void Start () {
		for (int i = 0; i < RoadNodes.Length; i++)
		{
			RoadNodes[i].name = i.ToString("X");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Awake()
	{
		RoadNodes = FindObjectsOfType<RoadNode>();
		GenerateConnections();
	}

	private void GenerateConnections()
	{
		foreach (var nodeA in RoadNodes.Where(node=>node.name == "A").ToList())
		{
			nodeA.Nodes = new List<RoadNode>();
			foreach (var nodeB in RoadNodes.Where(node=>node.name == "B" && Vector3.Distance(nodeA.transform.position, node.transform.position) < 3f ).ToList())
			{
				nodeA.ConnectNode(nodeB);
			}
		}
	}
}
