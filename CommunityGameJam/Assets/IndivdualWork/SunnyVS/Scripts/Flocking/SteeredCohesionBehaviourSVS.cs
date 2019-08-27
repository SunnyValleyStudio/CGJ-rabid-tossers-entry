using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour/SteeredCohesion")]
    public class SteeredCohesionBehaviourSVS : FlockingBehaviourSVS
    {

        public static Vector2 currentVelocity;
        public static float agentSmoothTime = 0.5f;


        public override Vector2 CalculateAgentMovement(FlockAgentSVS flockAgent, List<Transform> context, FlockSVS flock)
        {
            return FindMiddlePointBetweenNeighboursAndMoveThere(flockAgent, context, flock);
        }

        private static Vector2 FindMiddlePointBetweenNeighboursAndMoveThere(FlockAgentSVS flockAgent, List<Transform> context, FlockSVS flock)
        {
            if (flock.useFilter && flock.filter != null)
            {
                context = flock.filter.Filter(flockAgent, context);
            }

            Vector2 steeredCohesionMove = Vector2.zero;
            if (context.Count == 0)
            {
                return steeredCohesionMove;
            }

            

            foreach (var item in context)
            {
                steeredCohesionMove += (Vector2)item.position;
            }
            steeredCohesionMove /= context.Count;

            //offset from agent
            steeredCohesionMove -= (Vector2)flockAgent.transform.position;
            steeredCohesionMove = Vector2.SmoothDamp(flockAgent.transform.up, steeredCohesionMove, ref currentVelocity, agentSmoothTime);

            return steeredCohesionMove;
        }
    }
}