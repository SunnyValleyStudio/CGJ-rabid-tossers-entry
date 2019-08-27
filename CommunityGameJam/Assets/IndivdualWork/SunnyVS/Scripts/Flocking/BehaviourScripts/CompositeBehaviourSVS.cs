using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour/Composit")]
    public class CompositeBehaviourSVS : FlockingBehaviourSVS
    {
        public FlockingBehaviourSVS[] behaviours;
        //How much each behaviour type contributes to the movement
        public float[] behaviourWeights;
        public override Vector2 CalculateAgentMovement(FlockAgentSVS flockAgent, List<Transform> context, FlockSVS flock)
        {
            Vector2 move = Vector2.zero;
            for (int i = 0; i < Mathf.Min(behaviours.Length,behaviourWeights.Length); i++)
            {
                Vector2 partialMove = behaviours[i].CalculateAgentMovement(flockAgent, context, flock)*behaviourWeights[i];
                if(partialMove != Vector2.zero)
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