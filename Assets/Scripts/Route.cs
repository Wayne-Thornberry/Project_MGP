using System;
using System.Collections.Generic;
using System.Linq;

namespace New
{
    public class Route : List<RouteNode>
    {
        public int Weight {
            get
            {
                var w = 0;
                foreach (var node in this) w += node.Weight;
                return w;
            }
        }
        public bool IsSuccessful { get; set; }
        public float CalcTime { get; set; }
        public int Tries { get; set; }
        public int NodeCount
        {
            get
            {
                return this.Count;
            }
        }

        public bool Contains(RoadNode node)
        {
            foreach (var routeNode in this)
            {
                if (routeNode.Value == node) return true;
            }
            return false;
        }
        
        public override string ToString()
        {
            return " PathInfo{ [" + Weight + "]Successful: " + IsSuccessful + " Tries: " + this.Tries +
                   " Calculation Time: " + this.CalcTime + " ";
        }

        public string GetPathToString()
        {
            var path = "";
            foreach (var node in this) path = path + " => " + "[" + node.Value.Weight + "]" + " "+ "[" + node.Weight + "]" + node.Name;
            return "{"  + path + "}";
        }
    }
}