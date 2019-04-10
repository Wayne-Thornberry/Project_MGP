using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

public class AI : MonoBehaviour
{
    public Node Destination; // Where the AI wants to go


    public Node Home; // Starting Place of the AI
    public LinkedListNode<Node> NextTarget;

    public LinkedList<Node>
        Route; // The AI Route to Follow, they will go through all nodes (Linked list nodes) to reach the real nodes

    public AI()
    {
        Route = new LinkedList<Node>();
        Randoms = new Random();
        MaxSpeed = 10f;
        CarColor = new Color((float) Randoms.NextDouble(), (float) Randoms.NextDouble(), (float) Randoms.NextDouble());
    }

    public Color CarColor { get; set; }

    public Random Randoms { get; set; }

    public bool RunPath { get; set; }

    public float MaxSpeed { get; set; }
    public float Speed { get; set; }

    private void Start()
    {
        Home = World.Nodes[Randoms.Next(0, World.Nodes.Length)];
        Destination = World.Nodes[Randoms.Next(0, World.Nodes.Length)];
        gameObject.transform.position = Home.transform.position;
        gameObject.transform.rotation = Home.transform.rotation;
        GenerateRoute();
        NextTarget = Route.First;
        RunPath = true;
        foreach (var renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material.shader = Shader.Find("_Color");
            renderer.material.SetColor("_Color", CarColor);
            renderer.material.shader = Shader.Find("Specular");
            renderer.material.SetColor("_SpecColor", CarColor);
        }
    }

    public void FixedUpdate()
    {
        if (!RunPath) return;
        if (NextTarget != null)
        {
            //Debug.DrawLine(transform.position, NextTarget.Value.transform.position, Color.red);
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

                    gameObject.transform.position = gameObject.transform.position +
                                                    gameObject.transform.forward * (Time.deltaTime * Speed);
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
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        if (Route == null || NextTarget == null) return;
        var node = Route.First;
        while (node.Next != null)
        {
            Debug.DrawLine(node.Value.transform.position, node.Next.Value.transform.position, CarColor);
            node = node.Next;
        }
    }

    private void GenerateRoute()
    {
        var pathtime = Time.realtimeSinceStartup ;
        Route.AddFirst(Home); // starting point is the home I.E starting position
        var nodeBlacklist = new List<Node>();
        var iters = 0;
        try
        {
            while (Route.Last.Value != Destination && iters < 600) // keep calculating until you hit your destination
            {
                var currNode = Route.Last.Value;
                Node nextNode = null;
                foreach (var node in currNode.Nodes)
                {
                    if (node == null) continue;
                    if (Route.Contains(node) || nodeBlacklist.Contains(node)) continue;
                    if (nextNode == null)
                    {
                        nextNode = node;
                    }
                    else
                    {
                        if (node.GetWeight(Destination) < nextNode.GetWeight(Destination)) nextNode = node;
                    }
                }

                if (nextNode == null)
                {
                    nodeBlacklist.Add(Route.Last.Value);
                    Route.RemoveLast();
                }
                else
                {
                    Route.AddLast(nextNode);
                }

                iters++;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        var path = "";
        pathtime = Time.realtimeSinceStartup - pathtime;
        pathtime = (float) ((pathtime * 100) / 100.0);
        foreach (var node in Route) path = path + " => " + "[" + node.GetWeight(Destination) + "]" + node.name;
        Debug.Log("Car " + CarColor + " Home: " + " Path Time: " + pathtime + " " + Home.name + " Destination:" + Destination.name + " Tries: " + iters +
                  " " + path);
    }
}