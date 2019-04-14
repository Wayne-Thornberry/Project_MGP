using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
	public List<RoadNode> Nodes;
	public string Name;
	public int Weight;
	public bool IsSpawpoint;
	public bool IsDestination;
	public int Speedlimit;
	public bool IsTrafficLight;

	private TrafficLight _trafficLight;
	public RoadNode()
	{
		Nodes = new List<RoadNode>();
	}


	public void ConnectNode(RoadNode roadNode)
	{
		Nodes.Add(roadNode);
		this.transform.LookAt(roadNode.gameObject.transform);
	}

	public bool IsSelected { get; set; }

	private void OnDrawGizmos()
	{
		foreach (var connection in Nodes)
		{
			if(connection != null)
				Debug.DrawLine(transform.position, connection.transform.position, Color.green);
		}
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
		Handles.Label(transform.position, this.name);
	}

	private void Start()
	{
		if (IsTrafficLight)
		{
			AddModification("TrafficLight");
		}
	}

	private void AddModification(string modification)
	{
		switch (modification)
		{
			case "TrafficLight" : CreateTrafficLight(); break;
		}
	}

	private void CreateTrafficLight()
	{
		var tl = Instantiate(FindObjectOfType<ObjectController>().TrafficLight);
		tl.transform.position = this.gameObject.transform.position;
		tl.transform.Rotate(Vector3.up, 270f);
		_trafficLight = tl.GetComponentInParent<TrafficLight>();
		_trafficLight.OnLightChanged += () =>
		{
			Speedlimit = _trafficLight.Speed;
			Debug.Log(Speedlimit);
		};
	}

	public int GetWeight(RoadNode destination)
	{
		if(_trafficLight != null)
		return (int) Vector3.Distance(this.transform.position, destination.transform.position) + _trafficLight.Weight;
		return (int) Vector3.Distance(this.transform.position, destination.transform.position);
	}

}
