using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour/Alignment")]
    public class AlignmentSVS : FlockingBehaviourSVS
    {
        public override Vector2 CalculateAgentMovement(FlockAgentSVS flockAgent, List<Transform> context, FlockSVS flock)
        {
            return FindDirectionThatIsAnAverageOfAllNeighbours(flockAgent, context,flock);
        }

        private static Vector2 FindDirectionThatIsAnAverageOfAllNeighbours(FlockAgentSVS flockAgent, List<Transform> context, FlockSVS flock)
        {
            if (flock.useFilter && flock.filter != null)
            {
                context = flock.filter.Filter(flockAgent, context);
            }

            Vector2 alignmentMove = flockAgent.transform.up;
            if (context.Count == 0)
            {
                return alignmentMove;
            }

            
            alignmentMove = Vector2.zero;
            foreach (var item in context)
            {
                alignmentMove += (Vector2)item.transform.up;
            }
            alignmentMove /= context.Count;

            return alignmentMove;
        }
    }

}