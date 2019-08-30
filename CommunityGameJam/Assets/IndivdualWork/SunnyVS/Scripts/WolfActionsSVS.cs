using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVSFlocking;

namespace SVSWolf
{
    public class WolfActionsSVS : MonoBehaviour
    {
        public LayerMask sheepMask;
        public LayerMask wolfLair;
        public float wolfRadius = 3f;
        public float breakUpDistance = 10f;
        public float sheepSpeed = 6f;
        private GameObject sheepFollowing = null;
        public Flock3dSVS flock;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (sheepFollowing == null)
            {
                Collider[] contextColliders = Physics.OverlapSphere(transform.position, wolfRadius,sheepMask);
                if (contextColliders.Length > 0)
                {
                    sheepFollowing = contextColliders[0].gameObject;
                    flock = sheepFollowing.GetComponent<FlockAgent3dSVS>().AgentFlockGroup;
                    sheepFollowing.GetComponent<FlockAgent3dSVS>().isDead = true;
                    
                    sheepFollowing.GetComponent<FollowWolfSVS>().target = gameObject;
                    sheepFollowing.GetComponent<FollowWolfSVS>().speed = sheepSpeed;
                }
            }
            else
            {
                if (Vector3.Distance(sheepFollowing.transform.position, transform.position) > breakUpDistance)
                {
                    sheepFollowing.GetComponent<FollowWolfSVS>().target = null;

                    flock.AddAgent(sheepFollowing.GetComponent<FlockAgent3dSVS>());

                    sheepFollowing = null;
                    

                }
            }
        
        }

        private void OnTriggerStay(Collider other)
        {
            if(sheepFollowing!=null && other.gameObject.layer == Mathf.Log(wolfLair.value, 2))
            {
                sheepFollowing.SetActive(false);
                sheepFollowing = null;
                Debug.Log("Yummy!");
            }
        }
    }
}

