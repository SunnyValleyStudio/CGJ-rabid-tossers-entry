using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/AlignmentLost")]
    public class AlignmentLost3dSVS : FlockingBehaviour3dSVS
    {
        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            return FindDirectionThatIsAnAverageOfAllNeighbours(flockAgent, context,flock);
        }

        private static Vector3 FindDirectionThatIsAnAverageOfAllNeighbours(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            Vector3 alignmentMove = flockAgent.transform.forward;
            if (context.Count == 0)
            {
                return alignmentMove;
            }
            foreach(var element in context)
            {
                if (element.CompareTag("Player"))
                {
                    flockAgent.Initialize(element.GetComponent<Flock3dSVS>());
                }
            }
            return alignmentMove;
        }
    }

}