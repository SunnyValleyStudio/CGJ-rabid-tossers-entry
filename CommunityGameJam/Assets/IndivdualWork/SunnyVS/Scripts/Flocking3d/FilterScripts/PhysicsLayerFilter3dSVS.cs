using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Filters3d/PhysicsFilter")]
    public class PhysicsLayerFilter3dSVS : ContextFilter3dSVS
    {
        public LayerMask mask;
        public override List<Transform> Filter(FlockAgent3dSVS agent, List<Transform> context)
        {
            List<Transform> filtered = new List<Transform>();
            if (mask == 0)
            {
                return context;
            }
            foreach(Transform item in context)
            {
                //if (LayerMask.LayerToName(item.gameObject.layer)=="ObstacleSVS")
                //{
                //    Debug.Log(LayerMask.LayerToName(item.gameObject.layer));
                //    filtered.Add(item);
                //}
                if(mask == (mask | (1<< item.gameObject.layer)))
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }
}