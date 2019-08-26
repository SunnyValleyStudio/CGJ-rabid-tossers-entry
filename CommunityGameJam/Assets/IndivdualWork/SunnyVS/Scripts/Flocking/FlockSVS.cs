using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SVSFlocking
{
    public class FlockSVS : MonoBehaviour
    {
        public FlockAgentSVS agentPrefab;
        List<FlockAgentSVS> _agents = new List<FlockAgentSVS>();
        public FlockingBehaviourSVS behaviour;

        [Range(10, 500)]
        public int startingFlockSize = 50;
        const float _agentDensity = 0.08f;

        [Range(1f, 100f)]
        public float driveFactor = 10f;
        [Range(1f, 100f)]
        public float maxSpeed = 5f;
        [Range(1f, 10f)]
        public float neighbourRadious = 1.5f;
        [Range(0f, 1f)]
        public float avoidanceRadiousMultiplier = 0.5f;
        public ContextFilterSVS filter;
        public bool useFilter;
        public ContextFilterSVS physicsFilter;
        public bool usePhysicsFilter;

        float _squareMaxSpeed;
        float _squareNeighboursRadious;
        float _squareAvoidanceRadious;

        string[] names = { "Stuart", "Bob", "Kevin" };

        public float SquareAvoidanceRadious { get => _squareAvoidanceRadious; private set => _squareAvoidanceRadious = value; }

        void Start()
        {
            _squareMaxSpeed = maxSpeed * maxSpeed;
            _squareNeighboursRadious = neighbourRadious*neighbourRadious;
            
            SquareAvoidanceRadious = _squareNeighboursRadious * avoidanceRadiousMultiplier * avoidanceRadiousMultiplier;

            for (int i = 0; i < startingFlockSize; i++)
            {
                Vector3 position = Random.insideUnitCircle * startingFlockSize * _agentDensity;
                Quaternion rotation = Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f));
                FlockAgentSVS newAgent = Instantiate(agentPrefab, position, rotation, transform);
                int nameRandomChoiceVal = Random.Range(0, 3);
                newAgent.name = names[nameRandomChoiceVal] + i;
                newAgent.Initialize(this);
                _agents.Add(newAgent);
            }
        }

        // Update is called once per frame
        void Update()
        {
            MoveAgents();
        }

        private void MoveAgents()
        {
            foreach (FlockAgentSVS agent in _agents)
            {
                List<Transform> contextOfNeighbourRadious = GetNearbyObjects(agent);

                //TESTING
                //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, contextOfNeighbourRadious.Count / 6f);

                Vector2 movementVector = behaviour.CalculateAgentMovement(agent, contextOfNeighbourRadious, this);
                movementVector *= driveFactor;
                if (movementVector.sqrMagnitude > _squareMaxSpeed)
                {
                    movementVector = movementVector.normalized * _squareMaxSpeed;
                }
                agent.Move(movementVector);
            }
        }

        private List<Transform> GetNearbyObjects(FlockAgentSVS agent)
        {
            List<Transform> context = new List<Transform>();
            //3d Collider[] = ...OverlapSphere
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadious);
            foreach (var collider in contextColliders)
            {
                if(collider != agent.AgentCollider)
                {
                    context.Add(collider.transform);
                }
            }
            return context;
        }
    }
}

