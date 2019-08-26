using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour/Avoidance")]
    public class AvoidanceSVS : FlockingBehaviourSVS
    {
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
            Vector2 avoidanceMove = Vector2.zero;
            if (context.Count == 0)
            {
                return avoidanceMove;
            }


            

            int neighboursToAvoid = 0;
            foreach (Transform item in context)
            {
                if(Vector2.SqrMagnitude(item.position-flockAgent.transform.position) < flock.SquareAvoidanceRadious)
                {
                    neighboursToAvoid++;
                    avoidanceMove += (Vector2)(flockAgent.transform.position-item.position);
                    
                }
                
            }
            if (neighboursToAvoid > 0)
            {
                avoidanceMove /= neighboursToAvoid;
            }

            return avoidanceMove;
        }
    }
}
