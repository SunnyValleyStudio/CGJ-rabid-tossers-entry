using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Filters/SameFlockFilter")]
    public class SameFlockFilterSVS : ContextFilterSVS
    {
        public override List<Transform> Filter(FlockAgentSVS agent, List<Transform> context)
        {
            List<Transform> filtered = new List<Transform>();
            foreach (var item in context)
            {
                FlockAgentSVS itemAgent = item.GetComponent<FlockAgentSVS>();
                if (itemAgent!=null && itemAgent.AgentFlockGroup == agent.AgentFlockGroup)
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }
}
