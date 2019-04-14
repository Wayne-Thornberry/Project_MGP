using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnController : MonoBehaviour
    {
        public static bool Connected { get; set; }

        public GameObject AI;
        public static int Cars;
        private static float time;
        
        private void FixedUpdate()
        {
            time += Time.deltaTime;
            if (time > 0.5f && Cars < 10)
            {
                time = 0;
                var test = Instantiate(AI);
               Cars++;
            }
        }
    }
}