using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVSFlocking
{
    public abstract class ContextFilterSVS : ScriptableObject
    {
        public abstract List<Transform> Filter(FlockAgentSVS agent, List<Transform> context);
    }
}

