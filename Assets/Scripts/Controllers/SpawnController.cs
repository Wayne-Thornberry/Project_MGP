using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnController : MonoBehaviour
    {
        public static bool Connected { get; set; }
        public static RoadNode[] SpawnPoints { get; set; }
        public static RoadNode[] DestinationPoints { get; set; }
        
        public int CarLimit;
        public static int Cars;
        private static float time;

        private void Start()
        {
            SpawnPoints = ConnectionsController.RoadNodes.Where(i => i.IsSpawpoint).ToArray();
            DestinationPoints = ConnectionsController.RoadNodes.Where(i => i.IsDestination).ToArray();
        }

        private void FixedUpdate()
        {
            time += Time.deltaTime;
            if (time > 0.5f && Cars < CarLimit)
            {
                time = 0;
                var test = Instantiate(FindObjectOfType<ObjectController>().AI);
               Cars++;
            }
        }
    }
}