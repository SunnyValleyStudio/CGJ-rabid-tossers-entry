using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName ="SunnyVS/Flocking/Behaviour/Cohesion")]
    public class CohesionSVS : FlockingBehaviourSVS
    {
        public override Vector2 CalculateAgentMovement(FlockAgentSVS flockAgent, List<Transform> context, FlockSVS flock)
        {
            return FindMiddlePointBetweenNeighboursAndMoveThere(flockAgent, context);
        }

        private static Vector2 FindMiddlePointBetweenNeighboursAndMoveThere(FlockAgentSVS flockAgent, List<Transform> context)
        {
            Vector2 cohesionMove = Vector2.zero;
            if (context.Count == 0)
            {
                return cohesionMove;
            }

            foreach (var item in context)
            {
                cohesionMove += (Vector2)item.position;
            }
            cohesionMove /= context.Count;

            //offset from agent
            cohesionMove -= (Vector2)flockAgent.transform.position;

            return cohesionMove;
        }
    }
}
