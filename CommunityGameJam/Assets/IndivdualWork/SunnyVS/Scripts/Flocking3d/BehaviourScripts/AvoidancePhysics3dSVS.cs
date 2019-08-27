using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/AvoidancePhysics")]
    public class AvoidancePhysics3dSVS : FlockingBehaviour3dSVS
    {
        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            return FindMiddlePointBetweenNeighboursAndMoveThere(flockAgent, context, flock);
        }

        private static Vector3 FindMiddlePointBetweenNeighboursAndMoveThere(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            
            if (flock.usePhysicsFilter && flock.physicsFilter != null)
            {
                context = flock.physicsFilter.Filter(flockAgent, context);
            }
            Vector3 avoidanceMove = Vector3.zero;
            if (context.Count == 0)
            {
                return avoidanceMove;
            }


            

            int neighboursToAvoid = 0;
            foreach (Transform item in context)
            {
                if(Vector3.SqrMagnitude(item.position-flockAgent.transform.position) < flock.SquareAvoidanceRadious)
                {
                    neighboursToAvoid++;
                    avoidanceMove += (flockAgent.transform.position-item.position);
                    
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
