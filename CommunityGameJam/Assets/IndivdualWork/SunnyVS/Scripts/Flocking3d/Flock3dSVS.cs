using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SVSFlocking
{
    public class Flock3dSVS : MonoBehaviour
    {
        public FlockAgent3dSVS agentPrefab;
        List<FlockAgent3dSVS> _agents = new List<FlockAgent3dSVS>();
        public FlockingBehaviour3dSVS behaviour;

        [Range(0, 500)]
        public int startingFlockSize = 50;
        const float _agentDensity = 0.5f;

        [Range(1f, 100f)]
        public float driveFactor = 10f;
        [Range(1f, 100f)]
        public float maxSpeed = 5f;
        [Range(1f, 10f)]
        public float neighbourRadious = 1.5f;
        [Range(0f, 1f)]
        public float avoidanceRadiousMultiplier = 0.5f;
        public ContextFilter3dSVS filter;
        public bool useFilter;
        public ContextFilter3dSVS physicsFilter;
        public bool usePhysicsFilter;
        public GameObject[] otherGameobjectToFollow;

        float _squareMaxSpeed;
        float _squareNeighboursRadious;
        float _squareAvoidanceRadious;

        string[] names = { "Stuart", "Bob", "Kevin" };
        public float allowedDistanceFromFlockObject;
        public float timeBeforeSplit = 5f;
        public Flock3dSVS lostFlockGroup;
        public bool useSplit;
        float currentTime = 0f;


        public float SquareAvoidanceRadious { get => _squareAvoidanceRadious; private set => _squareAvoidanceRadious = value; }

        void Start()
        {
            _squareMaxSpeed = maxSpeed * maxSpeed;
            _squareNeighboursRadious = neighbourRadious*neighbourRadious;
            
            SquareAvoidanceRadious = _squareNeighboursRadious * avoidanceRadiousMultiplier * avoidanceRadiousMultiplier;

            for (int i = 0; i < startingFlockSize; i++)
            {
                Vector3 position = transform.position + Random.insideUnitSphere*4* startingFlockSize * _agentDensity;
                position = new Vector3(position.x, transform.position.y-0.8f, position.z);
                Quaternion rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));
                FlockAgent3dSVS newAgent = Instantiate(agentPrefab, position, rotation, transform);
                int nameRandomChoiceVal = Random.Range(0, 3);
                newAgent.name = names[nameRandomChoiceVal] + i;
                newAgent.Initialize(this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            MoveAgents();
            SplitAgent();
            CheckForDead();
        }

        private void SplitAgent()
        {
            if (useSplit)
            {
                currentTime += Time.deltaTime;
                if (currentTime > timeBeforeSplit)
                {
                    currentTime = 0;
                    FlockAgent3dSVS agentToSplit = _agents[_agents.Count - 1];
                    agentToSplit.Initialize(lostFlockGroup, true);


                }
            }
        }

        public void AlignToPlayer(List<Transform> context, FlockAgent3dSVS agent)
        {
            foreach (var element in context)
            {
                if (element.CompareTag("Player"))
                {
                    agent.Initialize(element.GetComponent<Flock3dSVS>());
                    break;
                }
            }
        }
        private void MoveAgents()
        {
            foreach (FlockAgent3dSVS agent in _agents)
            {
                if (agent.isDead)
                {
                    continue;
                }
                List<Transform> contextOfNeighbourRadious = GetNearbyObjects(agent);

                //TESTING
                //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, contextOfNeighbourRadious.Count / 6f);

                Vector3 movementVector = behaviour.CalculateAgentMovement(agent, contextOfNeighbourRadious, this);
                movementVector *= driveFactor;
                if (movementVector.sqrMagnitude > _squareMaxSpeed)
                {
                    movementVector = movementVector.normalized * maxSpeed;
                }
                agent.Move(new Vector3(movementVector.x,0, movementVector.z));
            }
        }

        private List<Transform> GetNearbyObjects(FlockAgent3dSVS agent)
        {
            List<Transform> context = new List<Transform>();
            //3d Collider[] = ...OverlapSphere
            Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadious);
            foreach (var collider in contextColliders)
            {
                if(collider != agent.AgentCollider)
                {
                    context.Add(collider.transform);
                }
            }
            return context;
        }

        public void AddAgent(FlockAgent3dSVS agent)
        {
            _agents.Add(agent);
        }

        public void RemoveAgent(FlockAgent3dSVS agent)
        {
            _agents.Remove(agent);
        }

        private void CheckForDead()
        {

            _agents = _agents.Where(x => !x.isDead).ToList();
        }
    }
}

