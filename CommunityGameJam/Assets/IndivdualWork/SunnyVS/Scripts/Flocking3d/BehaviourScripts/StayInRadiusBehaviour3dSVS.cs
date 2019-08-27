using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/StayInRadious")]
    public class StayInRadiusBehaviour3dSVS : FlockingBehaviour3dSVS
    {
        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            Vector3 centerOffset = flock.transform.position - flockAgent.transform.position;
            float positionReleventToCenter01 = centerOffset.magnitude / flock.allowedDistanceFromFlockObject;
            if(positionReleventToCenter01 < 0.9f)
            {
                return Vector3.zero;
            }
            return centerOffset * positionReleventToCenter01 * positionReleventToCenter01;
        }
    }
}