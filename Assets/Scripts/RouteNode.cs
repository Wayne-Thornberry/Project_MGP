using System.Collections.Generic;

namespace New
{
    public class RouteNode
    {
        public string Name { get; set; }
        public int Weight;
        public RoadNode Value;
        public List<RoadNode> Blacklist;

        public RouteNode(RoadNode node, int weight)
        {
            Blacklist = new List<RoadNode>();
            Name = node.name;
            Weight = weight;
            Value = node;
        }

    }
}