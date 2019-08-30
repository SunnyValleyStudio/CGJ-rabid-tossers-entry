using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVSInput
{
    public class PlayerInputSVS : MonoBehaviour
    {
        float horizontal;
        float vertical;
        public CharacterController controller;
        public float rotationSpeed = 3.0f;
        public float speed = 3.0f;
        public bool movementBlockDuringFIght = false;

        // Update is called once per frame
        void Update()
        {
            // Jonu - added an axis for the wolf's movement - WASD
            horizontal = Input.GetAxis("WolfHorizontal");
            vertical = Input.GetAxis("WolfVertical");
            
        }

        private void FixedUpdate()
        {
            if (movementBlockDuringFIght == false)
            {
                transform.Rotate(0, horizontal * rotationSpeed, 0);
                float curSpeed = speed * vertical;
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                controller.SimpleMove(forward * curSpeed);
            }
            
        }
    }
}

