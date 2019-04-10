using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace DefaultNamespace
{
    public class World : MonoBehaviour
    {
        public static Node[] Nodes { get; set; }
        public static bool Connected { get; set; }

        public GameObject AI;
        private static float time;

        public World()
        {
            
        }

        private void Awake()
        {
            Nodes = FindObjectsOfType<Node>();
            GenerateConnections();
        }

        private void GenerateConnections()
        {
            foreach (var nodeA in Nodes.Where(node=>node.name == "A").ToList())
            {
                foreach (var nodeB in Nodes.Where(node=>node.name == "B" && Vector3.Distance(nodeA.transform.position, node.transform.position) < 3f ).ToList())
                {
                    nodeA.ConnectNode(nodeB);
                }
            }
        }

        private void Update()
        {
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                time = 0;
                var test = Instantiate(AI);
            }
        }

        private void Start()
        {
            
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i].name = i.ToString("X");
            }
        }
    }
}