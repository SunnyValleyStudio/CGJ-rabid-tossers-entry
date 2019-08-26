using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Filters/PhysicsFilter")]
    public class PhysicsLayerFilterSVS : ContextFilterSVS
    {
        public LayerMask mask;
        public override List<Transform> Filter(FlockAgentSVS agent, List<Transform> context)
        {
            List<Transform> filtered = new List<Transform>();
            if (mask == 0)
            {
                return context;
            }
            foreach(Transform item in context)
            {
                if(mask == (mask | (1<< item.gameObject.layer)))
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }
}