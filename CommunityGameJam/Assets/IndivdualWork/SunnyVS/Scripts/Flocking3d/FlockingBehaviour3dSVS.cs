using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVSFlocking
{
    public abstract class FlockingBehaviour3dSVS : ScriptableObject
    {
        public abstract Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock);
    }
}
