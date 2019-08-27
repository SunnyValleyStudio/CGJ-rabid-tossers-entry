using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/Composit")]
    public class CompositeBehaviour3dSVS : FlockingBehaviour3dSVS
    {
        public FlockingBehaviour3dSVS[] behaviours;
        //How much each behaviour type contributes to the movement
        public float[] behaviourWeights;
        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            Vector3 move = Vector3.zero;
            for (int i = 0; i < Mathf.Min(behaviours.Length,behaviourWeights.Length); i++)
            {
                Vector3 partialMove = behaviours[i].CalculateAgentMovement(flockAgent, context, flock)*behaviourWeights[i];
                if(partialMove != Vector3.zero)
                {
                    if(partialMove.sqrMagnitude > behaviourWeights[i] * behaviourWeights[i])
                    {
                        partialMove.Normalize();
                        partialMove *= behaviourWeights[i];
                    }

                    move += partialMove;
                }
            }

            return move;
        }
    }
}