using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

public class AI : MonoBehaviour
{
    public RoadNode Home; // Starting Place of the AI
    public RoadNode Destination; // Where the AI wants to go
    
    public LinkedListNode<RoadNode> NextTarget;
    public LinkedList<RoadNode> Route; // The AI Route to Follow, they will go through all nodes (Linked list nodes) to reach the real nodes

    public AI()
    {
        Route = new LinkedList<RoadNode>();
        Randoms = new Random();
        MaxSpeed = 10f;
        CarColor = new Color((float) Randoms.NextDouble(), (float) Randoms.NextDouble(), (float) Randoms.NextDouble());
    }

    public Color CarColor { get; set; }
    public Random Randoms { get; set; }
    public bool RunPath { get; set; }
    public float MaxSpeed { get; set; }
    public float Speed { get; set; }
    public bool IsFocused { get; set; }

    private void Start()
    {
        if(Home == null)
        Home = ConnectionsController.RoadNodes[Randoms.Next(0, ConnectionsController.RoadNodes.Length)];
        
        if(Destination == null)
        Destination = ConnectionsController.RoadNodes[Randoms.Next(0, ConnectionsController.RoadNodes.Length)];
        
        gameObject.transform.position = Home.transform.position;
        gameObject.transform.rotation = Home.transform.rotation;
        Route = GeneratePath(Home,Destination);
        NextTarget = Route.First;
        RunPath = true;
        name = CarColor.ToString();
        foreach (var renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material.shader = Shader.Find("_Color");
            renderer.material.SetColor("_Color", CarColor);
            renderer.material.shader = Shader.Find("Specular");
            renderer.material.SetColor("_SpecColor", CarColor);
        }
    }

    public override string ToString()
    {
        return "Car " + CarColor + " Home: " + Home.name + " Destination:" + Destination.name + " " + Route;
    }

    public void FixedUpdate()
    {
        if (!RunPath) return;
        if (NextTarget != null)
        {
            if (Vector3.Distance(transform.position, NextTarget.Value.transform.position) > 0.1f)
            {
                RaycastHit hit;
                transform.LookAt(NextTarget.Value.transform.position);
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10, 3))
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20,
                        3))
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20,
                            Color.yellow);
                        if (hit.distance < 10 && hit.distance > 5)
                            Speed -= 0.1f;
                        else if (hit.distance < 5) Speed -= 1f;

                        if (Speed <= 0) Speed = 0;
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20,
                            Color.white);
                        Speed += 0.1f;
                        if (Speed > MaxSpeed) Speed = MaxSpeed;
                    }

                    gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward * (Time.deltaTime * Speed);
                }
            }
            else
            {
                NextTarget = NextTarget.Next;
            }
        }
        else
        {
            RunPath = false;
            Kill();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        if (Route == null || NextTarget == null || !IsFocused) return;
        var node = Route.First;
        while (node.Next != null)
        {
            Debug.DrawLine(node.Value.transform.position, node.Next.Value.transform.position, CarColor);
            node = node.Next;
        }
    }

    private Path GeneratePath(RoadNode home, RoadNode destination)
    {
        var path = new Path();
        path.CalcTime = Time.realtimeSinceStartup;
        path.AddFirst(home);
        RoadNode currRoadNode;
        RoadNode nextRoadNode;
        while (path.Last.Value != destination && path.Tries < 600)
        {
            currRoadNode = path.Last.Value;
            nextRoadNode = null;
            
            foreach (var node in currRoadNode.Nodes)
            {
                if (node == null || path.Contains(node)) continue;
                if (nextRoadNode == null)
                {
                    nextRoadNode = node;
                }
                else
                {
                    if (node.GetWeight(destination) < nextRoadNode.GetWeight(destination)) nextRoadNode = node;
                }
            }

            if (nextRoadNode != null)
            {
                path.AddLast(nextRoadNode);
            }
            else
            {
                break;
            }
            path.Tries++;
        } 
        
        path.CalcTime = Time.realtimeSinceStartup - path.CalcTime;
        path.CalcTime = (float) ((path.CalcTime * 100) / 100.0);
        if (path.Last.Value == destination)
        {
            path.IsSuccessful = true;
        }

        return path;
    }

    public void Kill()
    {
        SpawnController.Cars--;
        Destroy(gameObject);
    }
}