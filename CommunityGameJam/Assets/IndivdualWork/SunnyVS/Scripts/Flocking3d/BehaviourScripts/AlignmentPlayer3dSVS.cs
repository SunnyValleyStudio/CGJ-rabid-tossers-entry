using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/AlignmentPlayer")]
    public class AlignmentPlayer3dSVS : FlockingBehaviour3dSVS
    {
        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            return FindDirectionThatIsAnAverageOfAllNeighbours(flockAgent, context,flock);
        }

        private static Vector3 FindDirectionThatIsAnAverageOfAllNeighbours(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            
            
            Vector3 alignmentMove = Vector3.zero;

                alignmentMove += flock.transform.forward;
            


            return alignmentMove;
        }
    }

}