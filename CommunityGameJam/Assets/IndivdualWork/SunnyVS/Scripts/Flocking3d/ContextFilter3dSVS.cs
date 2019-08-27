using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVSFlocking
{
    public abstract class ContextFilter3dSVS : ScriptableObject
    {
        public abstract List<Transform> Filter(FlockAgent3dSVS agent, List<Transform> context);
    }
}

