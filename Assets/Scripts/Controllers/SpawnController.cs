using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnController : MonoBehaviour
    {
        public static bool Connected { get; set; }
        public static List<AI> WorldCars { get; set; }
        public static RoadNode[] SpawnPoints { get; set; }
        public static RoadNode[] DestinationPoints { get; set; }
        public static bool DisableSpawning { get; set; }

        public int CarLimit;
        private float Time;
        public static int Cars;
        public float SpawnTime;

        private void Start()
        {
            SpawnPoints = ConnectionsController.RoadNodes.Where(i => i.IsSpawpoint).ToArray();
            DestinationPoints = ConnectionsController.RoadNodes.Where(i => i.IsDestination).ToArray();
            WorldCars = new List<AI>();
            if (GameConfig.UseCustom)
            {
                CarLimit = GameConfig.CarLimit;
                SpawnTime = GameConfig.CarIntervol;
            }
        }

        private void FixedUpdate()
        {
            if(DisableSpawning) return;;
            Time += UnityEngine.Time.deltaTime;
            if (Time > SpawnTime && Cars < CarLimit)
            {
                Time = 0;
                var ai = Instantiate(FindObjectOfType<ObjectController>().AI);
                WorldCars.Add(ai.GetComponent<AI>());
                Cars++;
            }
        }

    }
}