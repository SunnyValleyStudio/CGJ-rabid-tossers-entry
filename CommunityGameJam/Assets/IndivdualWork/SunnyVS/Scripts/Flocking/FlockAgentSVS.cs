using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [RequireComponent(typeof(Collider2D))]
    public class FlockAgentSVS : MonoBehaviour
    {
        private Collider2D _agentCollider;
        FlockSVS agentFlockGroup;

        public Collider2D AgentCollider { get => _agentCollider; private set => _agentCollider = value; }
        public FlockSVS AgentFlockGroup { get => agentFlockGroup; set => agentFlockGroup = value; }

        private void Awake()
        {
            AgentCollider = GetComponent<Collider2D>();
        }

        public void Initialize(FlockSVS flock)
        {
            AgentFlockGroup = flock;
        }

        public void Move(Vector2 velocity)
        {
            //3d transform.foreward
            transform.up = velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
    }
}
