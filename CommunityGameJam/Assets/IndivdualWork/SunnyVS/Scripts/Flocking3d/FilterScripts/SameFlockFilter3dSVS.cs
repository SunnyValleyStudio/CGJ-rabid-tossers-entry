using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Filters3d/SameFlockFilter")]
    public class SameFlockFilter3dSVS : ContextFilter3dSVS
    {
        public override List<Transform> Filter(FlockAgent3dSVS agent, List<Transform> context)
        {
            List<Transform> filtered = new List<Transform>();
            foreach (var item in context)
            {
                FlockAgent3dSVS itemAgent = item.GetComponent<FlockAgent3dSVS>();
                if (itemAgent!=null && itemAgent.AgentFlockGroup == agent.AgentFlockGroup)
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }
}
