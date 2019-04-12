using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : LinkedList<RoadNode> {
    public Path()
    {
        
    }

    public override string ToString()
    {
        var path = "";
        foreach (var node in this) path = path + " => " + "[" + node.GetWeight(this.Last.Value) + "]" + node.name;
        return " PathInfo{ Successful: " + IsSuccessful + " Tries: " + this.Tries + " Calculation Time: " + this.CalcTime +  " " + path + "}";
    }

    public bool IsSuccessful { get; set; }
    public float CalcTime { get; set; }
    public int Tries { get; set; }
}
