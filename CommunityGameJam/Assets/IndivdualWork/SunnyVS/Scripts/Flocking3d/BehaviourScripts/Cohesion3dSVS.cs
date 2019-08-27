using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName ="SunnyVS/Flocking/Behaviour3d/Cohesion")]
    public class Cohesion3dSVS : FlockingBehaviour3dSVS
    {
        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            return FindMiddlePointBetweenNeighboursAndMoveThere(flockAgent, context);
        }

        private static Vector3 FindMiddlePointBetweenNeighboursAndMoveThere(FlockAgent3dSVS flockAgent, List<Transform> context)
        {
            Vector3 cohesionMove = Vector3.zero;
            if (context.Count == 0)
            {
                return cohesionMove;
            }

            foreach (var item in context)
            {
                cohesionMove += (Vector3)item.position;
            }
            cohesionMove /= context.Count;

            //offset from agent
            cohesionMove -= flockAgent.transform.position;

            return cohesionMove;
        }
    }
}
