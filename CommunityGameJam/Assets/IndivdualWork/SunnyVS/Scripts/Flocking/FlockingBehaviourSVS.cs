using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVSFlocking
{
    public abstract class FlockingBehaviourSVS : ScriptableObject
    {
        public abstract Vector2 CalculateAgentMovement(FlockAgentSVS flockAgent, List<Transform> context, FlockSVS flock);
    }
}
