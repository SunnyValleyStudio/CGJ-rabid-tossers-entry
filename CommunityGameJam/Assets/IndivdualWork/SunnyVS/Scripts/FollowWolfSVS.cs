using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVSWolf
{
    public class FollowWolfSVS : MonoBehaviour
    {
        public float speed = 1.5f;
        public float offsetFromTarget = 3f;
        
        public GameObject target;
        // Start is called before the first frame update
        void Start()
        {

        }

        
        // Update is called once per frame
        void FixedUpdate()
        {
            if (target != null)
            {
                if (Vector3.Distance(target.transform.position, transform.position) > offsetFromTarget)
                {
                    transform.LookAt(target.transform);
                    transform.position += transform.forward * speed * Time.deltaTime; ;
                }

                
            }
            
        }
    }
}

