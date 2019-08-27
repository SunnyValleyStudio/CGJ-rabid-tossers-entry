using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/AvoidancePlayer")]
    public class AvoidancePlayer3dSVS : FlockingBehaviour3dSVS
    {
        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            return FindMiddlePointBetweenNeighboursAndMoveThere(flockAgent, context, flock);
        }

        private static Vector3 FindMiddlePointBetweenNeighboursAndMoveThere(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            

            Vector3 avoidanceMove = Vector3.zero;

                avoidanceMove += (flockAgent.transform.position - flock.transform.position);


            return avoidanceMove;
        }
    }
}
