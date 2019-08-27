using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/SteeredCohesion")]
    public class SteeredCohesionBehaviour3dSVS : FlockingBehaviour3dSVS
    {

        public static Vector3 currentVelocity;
        public static float agentSmoothTime = 0.5f;


        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            return FindMiddlePointBetweenNeighboursAndMoveThere(flockAgent, context, flock);
        }

        private static Vector3 FindMiddlePointBetweenNeighboursAndMoveThere(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            if (flock.useFilter && flock.filter != null)
            {
                context = flock.filter.Filter(flockAgent, context);
            }

            Vector3 steeredCohesionMove = Vector3.zero;
            if (context.Count == 0)
            {
                return steeredCohesionMove;
            }

            

            foreach (var item in context)
            {
                steeredCohesionMove += item.position;
            }
            steeredCohesionMove /= context.Count;

            //offset from agent
            steeredCohesionMove -= flockAgent.transform.position;
            steeredCohesionMove = Vector3.SmoothDamp(flockAgent.transform.forward, steeredCohesionMove, ref currentVelocity, agentSmoothTime);

            return steeredCohesionMove;
        }
    }
}