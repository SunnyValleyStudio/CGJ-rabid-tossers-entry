using SVSAI;
using SVSWolf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SVSFlocking
{
    [RequireComponent(typeof(Collider))]
    public class FlockAgent3dSVS : MonoBehaviour, IAttackableSVS
    {
        private Collider _agentCollider;
        Flock3dSVS agentFlockGroup;
        public Flock3dSVS originFlock;
        private bool lost;
        public bool isDead;
        public Collider AgentCollider { get => _agentCollider; private set => _agentCollider = value; }
        public Flock3dSVS AgentFlockGroup { get => agentFlockGroup; set => agentFlockGroup = value; }
        private CharacterController movementController;
        NavMeshAgent agentAI = null;
        bool isREturningHome = false;
        private void Awake()
        {
            AgentCollider = GetComponent<Collider>();
            //movementController = GetComponent<CharacterController>();
            agentAI = GetComponent<NavMeshAgent>();
            agentAI.enabled = false;
        }

        public void Initialize(Flock3dSVS flock, bool lost = false)
        {
            if (agentFlockGroup != null)
            {
                agentFlockGroup.RemoveAgent(this);
            }
            AgentFlockGroup = flock;
            flock.AddAgent(this);
        }

        public void Move(Vector3 velocity)
        {
            //3d transform.forward
            transform.forward = velocity;
            transform.position += velocity * Time.deltaTime;
            //movementController.Move(velocity * Time.deltaTime);
        }

        public bool GetIsLost()
        {
            return lost;
        }

        private void OnDrawGizmosSelected()
        {
            if (agentFlockGroup != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(transform.position, agentFlockGroup.neighbourRadious);
            }
            
        }

        public void OnWInFight()
        {
            StopFollowingWolf();
            
        }

        public void OnLoseFight()
        {
            Destroy(gameObject);
            Debug.Log("Sheep eaten - add points UI");
        }

        private void Update()
        {
            if (isREturningHome)
            {
                if(Vector3.Distance(transform.position, originFlock.transform.position) < originFlock.allowedDistanceFromFlockObject)
                {
                    if (agentAI.isActiveAndEnabled)
                    {
                        agentAI.isStopped = true;
                        agentAI.enabled = false;
                    }
                    ReaddAgentToOldFlock();
                    isREturningHome = false;
                }
            }
        }

        public void StartFollowingWOlf(GameObject wolfObject, float sheepFollowSpeed)
        {
            isDead = true;
            isREturningHome = false;
            GetComponent<FollowWolfSVS>().target = wolfObject;
            GetComponent<FollowWolfSVS>().speed = sheepFollowSpeed;
            if (agentAI.isActiveAndEnabled)
            {
                agentAI.isStopped = true;
                agentAI.enabled = false;
            }
        }
        public void StopFollowingWolf()
        {
            GetComponent<FollowWolfSVS>().target = null;
            isREturningHome = true;
            agentAI.enabled = true;
            agentAI.isStopped = false;
            agentAI.SetDestination(originFlock.transform.position);
            

        }
        public void ReaddAgentToOldFlock()
        {
            originFlock.AddAgent(this);
        }
    }
}
