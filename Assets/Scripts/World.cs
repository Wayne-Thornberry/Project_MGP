using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace DefaultNamespace
{
    public class World : MonoBehaviour
    {
        public static RoadNode[] RoadNodes { get; set; }
        public static bool Connected { get; set; }

        public GameObject AI;
        public static int Cars;
        private static float time;

        public World()
        {
            
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

        private void FixedUpdate()
        {
            //time += Time.deltaTime;
            //if (time > 0.5f && Cars < 10)
            //{
            //    time = 0;
            //    var test = Instantiate(AI);
            //    Cars++;
            //}
        }

        private void Start()
        {
            
            for (int i = 0; i < RoadNodes.Length; i++)
            {
                RoadNodes[i].name = i.ToString("X");
            }
        }
    }
}