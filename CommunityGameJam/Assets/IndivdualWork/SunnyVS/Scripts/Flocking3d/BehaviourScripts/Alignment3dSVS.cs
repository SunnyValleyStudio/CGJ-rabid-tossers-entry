using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/Alignment")]
    public class Alignment3dSVS : FlockingBehaviour3dSVS
    {
        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            return FindDirectionThatIsAnAverageOfAllNeighbours(flockAgent, context,flock);
        }

        private static Vector3 FindDirectionThatIsAnAverageOfAllNeighbours(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            if (flock.useFilter && flock.filter != null)
            {
                context = flock.filter.Filter(flockAgent, context);
            }

            Vector3 alignmentMove = flockAgent.transform.forward;
            if (context.Count == 0)
            {
                return alignmentMove;
            }

            
            alignmentMove = Vector3.zero;
            foreach (var item in context)
            {
                alignmentMove += item.transform.forward;
            }
            alignmentMove /= context.Count;

            return alignmentMove;
        }
    }

}