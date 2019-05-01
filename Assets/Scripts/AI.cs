using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using New;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class AI : MonoBehaviour
{

    public AI()
    {
        Randoms = new Random();
        VehicleColor = new Color((float) Randoms.NextDouble(), (float) Randoms.NextDouble(), (float) Randoms.NextDouble());
        AccelerationSpeed = 0.1f + (float) Randoms.NextDouble();
    }
    public static bool EnableCarDespawning;

    // Route Information
    public Route SelectedRoute { get; set; }
    public List<Route> Routes { get; set; }
    public RoadNode Home; // Starting Place of the AI
    public RoadNode Destination; // Where the AI wants to go
    public RouteNode NextDestination;
    public int RouteProgress { get; set; }
    
    // AI Info
    public Color VehicleColor { get; set; }
    public Random Randoms { get; set; }
    public bool RunPath { get; set; }
    public float MaxSpeed { get; set; }
    public bool IsFocused { get; set; }
    public float AccelerationSpeed { get; set; }
    public float IdleTime { get; set; }

    private void Start()
    {
        if(Home == null)
        Home = SpawnController.SpawnPoints[Randoms.Next(0, SpawnController.SpawnPoints.Length)];

        if (Home.IsBlocked)
            Kill();
        
        if(Destination == null)
        Destination = SpawnController.DestinationPoints[Randoms.Next(0, SpawnController.SpawnPoints.Length)];
        
        gameObject.transform.position = Home.transform.position;
        gameObject.transform.rotation = Home.transform.rotation;
        Routes = GenerateRoutes(Home, Destination);
        SelectedRoute = GameConfig.UseDijkstra ? Routes.OrderBy(i=>i.Weight).FirstOrDefault() : Routes.OrderBy(i=>i.NodeCount).LastOrDefault();
        if(SelectedRoute == null) Kill();
        NextDestination = SelectedRoute.First();
        RunPath = true;
        name = VehicleColor.ToString();
        foreach (var r in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            r.material.shader = Shader.Find("_Color");
            r.material.SetColor("_Color", VehicleColor);
            r.material.shader = Shader.Find("Specular");
            r.material.SetColor("_SpecColor", VehicleColor);
        }

        SpawnController.Log += this + "\n";
    }


    public override string ToString()
    {
        return "Car " + VehicleColor + " Home: " + Home.name + " Destination:" + Destination.name + " Next Node Speed: " + NextDestination.Value.Speedlimit +  " \n" + SelectedRoute;
    }
    
    public void FixedUpdate()
    {
        if (!RunPath) return;

        if (EnableCarDespawning)
        {
            if (MaxSpeed > 0)
            {
                IdleTime = 0;
            }
            else
            {
                IdleTime += Time.deltaTime;
                if (IdleTime > 10f)
                {
                    Kill();
                }
            }
        }

        if (NextDestination != null)
        {
            if (Vector3.Distance(transform.position, NextDestination.Value.transform.position) > 2f)
            {
                RaycastHit hit;
                transform.LookAt(NextDestination.Value.transform.position);
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10, 3))
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20,
                        3))
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20,
                            Color.yellow);
                        if (hit.distance < 10 && hit.distance > 5)
                            MaxSpeed -= 0.1f;
                        else if (hit.distance < 5) MaxSpeed -= 2f;

                        if (MaxSpeed <= 0) MaxSpeed = 0;
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20,
                            Color.white);
                        MaxSpeed += AccelerationSpeed;
                        if (MaxSpeed > NextDestination.Value.Speedlimit) MaxSpeed = NextDestination.Value.Speedlimit;
                    }

                    gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward * (Time.deltaTime * MaxSpeed);
                }
            }
            else
            {
                RouteProgress++;
                NextDestination.Value.IsBlocked = false;
                NextDestination.Value.Weight -= 20;
                NextDestination = SelectedRoute.ElementAtOrDefault(RouteProgress);
                if (NextDestination == null) return;
                NextDestination.Value.IsBlocked = true;
                NextDestination.Value.Weight += 20;
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
        Gizmos.color = VehicleColor;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        Gizmos.DrawCube(Destination.transform.position, new Vector3(1,1,1));
        if (SelectedRoute == null || NextDestination == null || !IsFocused) return;
        for (int i = 0; i < SelectedRoute.Count; i++)
        {
            try
            {
                var node = SelectedRoute[i];
                var next = SelectedRoute[i+1];
                Debug.DrawLine(node.Value.transform.position, next.Value.transform.position, VehicleColor);
                Handles.Label(node.Value.transform.position, node.Value.name);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
    
    private List<Route> GenerateRoutes(RoadNode home, RoadNode destination)
    {
        var currRoute = new Route(); // The calculation route
        currRoute.CalcTime = Time.realtimeSinceStartup;
        var currRoadNode = home; // Starting point
        var currRouteNode = new RouteNode(currRoadNode, currRoadNode.GetWeightBasedOnDestination(destination)); // Translated into a useable infromation node
        currRoute.Add(currRouteNode); // Add it to the current route
        
        var routes = new List<Route>();
        var attempts = 0;
        try
        {
            while (attempts < 100)
        {
            while (!currRoute.IsSuccessful && currRoute.Tries < 600)
            {
                currRouteNode = currRoute.Last();
                currRoadNode = currRouteNode.Value;

                RoadNode nextRoadNode = null; // Next POI
                RouteNode nextRouteNode = null;

                foreach (var node in currRoadNode.Nodes)
                {
                    if (node == null || currRoute.Contains(node) || currRouteNode.Blacklist.Contains(node)) continue;
                    if (nextRoadNode == null)
                    {
                        nextRoadNode = node;
                    }
                    else
                    {
                        if (node.GetWeightBasedOnDestination(destination) < nextRoadNode.GetWeightBasedOnDestination(destination)) nextRoadNode = node;
                    }
                }

                if (nextRoadNode == null)
                {
                    currRoute.Remove(currRouteNode);
                    currRoute.Last().Blacklist.Add(currRoadNode);
                }
                else
                {
                    nextRouteNode = new RouteNode(nextRoadNode, nextRoadNode.GetWeightBasedOnDestination(destination));
                    currRoute.Add(nextRouteNode);
                    if (nextRouteNode.Value == destination)
                    {
                        currRoute.IsSuccessful = true;
                        currRoute.CalcTime = Time.realtimeSinceStartup - currRoute.CalcTime;
                        currRoute.CalcTime = (float) ((currRoute.CalcTime * 100) / 100.0);
                    }
                }

                currRoute.Tries++;
            }

            if (currRoute.IsSuccessful)
            {
                routes.Add(CopyRoute(currRoute));
                
                var lastRouteNode = currRoute.Last();
                var lastRoadNode = lastRouteNode.Value;
                currRoute.Remove(lastRouteNode);
                currRoute.Last().Blacklist.Add(lastRoadNode);
                currRoute.IsSuccessful = false;
                currRoute.Tries = 0;
                currRoute.CalcTime = Time.realtimeSinceStartup;
            }
                attempts++;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        if (routes.Count == 0) return null;
        Debug.Log("Home " +home.name + " To " + destination.name + " Routes found: " +routes.Count + " ");
        return routes;
    }

    private Route CopyRoute(Route route)
    {
        var newRoute = new Route();
        foreach (var node in route)
        {
            newRoute.Add(new RouteNode(node.Value, node.Weight));   
        }
        newRoute.IsSuccessful = route.IsSuccessful;
        newRoute.Tries = route.Tries;
        newRoute.CalcTime = route.CalcTime;
        return newRoute;
    }

    public void Kill()
    {
        SpawnController.Cars--;
        SpawnController.WorldCars.Remove(this);
        Destroy(gameObject);
    }
}