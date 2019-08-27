using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [CreateAssetMenu(menuName = "SunnyVS/Flocking/Behaviour3d/SteeredCohesionPlayer")]
    public class SteeredCohesionBehaviourPlayer3dSVS : FlockingBehaviour3dSVS
    {

        public static Vector3 currentVelocity;
        public static float agentSmoothTime = 0.5f;


        public override Vector3 CalculateAgentMovement(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            return FindMiddlePointBetweenNeighboursAndMoveThere(flockAgent, context, flock);
        }

        private static Vector3 FindMiddlePointBetweenNeighboursAndMoveThere(FlockAgent3dSVS flockAgent, List<Transform> context, Flock3dSVS flock)
        {
            Vector3 steeredCohesionMove = new Vector3();

                steeredCohesionMove += flock.transform.position;
            

            //offset from agent
            steeredCohesionMove -= flockAgent.transform.position;
            steeredCohesionMove = Vector3.SmoothDamp(flockAgent.transform.forward, steeredCohesionMove, ref currentVelocity, agentSmoothTime);

            return steeredCohesionMove;
        }
    }
}