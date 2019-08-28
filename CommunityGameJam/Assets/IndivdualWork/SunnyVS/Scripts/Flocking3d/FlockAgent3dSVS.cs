﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SVSFlocking
{
    [RequireComponent(typeof(Collider))]
    public class FlockAgent3dSVS : MonoBehaviour
    {
        private Collider _agentCollider;
        Flock3dSVS agentFlockGroup;
        private bool lost;
        public bool isDead;
        public Collider AgentCollider { get => _agentCollider; private set => _agentCollider = value; }
        public Flock3dSVS AgentFlockGroup { get => agentFlockGroup; set => agentFlockGroup = value; }

        private void Awake()
        {
            AgentCollider = GetComponent<Collider>();
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
        }

        public bool GetIsLost()
        {
            return lost;
        }
    }
}
