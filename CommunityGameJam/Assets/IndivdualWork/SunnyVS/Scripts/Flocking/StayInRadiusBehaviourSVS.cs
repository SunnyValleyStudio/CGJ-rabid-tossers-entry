using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour/StayInRadious")]
    public class StayInRadiusBehaviourSVS : FlockingBehaviourSVS
    {
        public Vector2 Center;
        public float radious = 15f;
        public override Vector2 CalculateAgentMovement(FlockAgentSVS flockAgent, List<Transform> context, FlockSVS flock)
        {
            Vector2 centerOffset = Center - (Vector2)flockAgent.transform.position;
            float positionReleventToCenter01 = centerOffset.magnitude / radious;
            if(positionReleventToCenter01 < 0.9f)
            {
                return Vector2.zero;
            }
            return centerOffset * positionReleventToCenter01 * positionReleventToCenter01;
        }
    }
}