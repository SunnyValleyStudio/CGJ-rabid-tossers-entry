using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Create an empty object and set it to "target" in this script and to "Follow" and "LookAt" in CM VCam

public class CameraControl : MonoBehaviour
{
    /// <summary>
    /// The player, main and starting target of the camera
    /// </summary>
    public Transform player;

    /// <summary>
    /// The object that cinemachine is following
    /// </summary>
    public Transform target;


    /// <summary>
    /// The Cinemachine virtual camera
    /// </summary>
    public CinemachineVirtualCamera CMVCam;

    /// <summary>
    /// The speed of target's movement (def 60)
    /// </summary>
    public float movementSpeed = 50;

    /// <summary>
    /// The speed of camera rotation
    /// </summary>
    public float rotationSpeed = 70;
    /// <summary>
    /// The amount of damping set to the cinemachine when rotating
    /// </summary>
    public float rotationDamping;

    // Zoom min, max and speed
    public float zoomMax = 15;
    public float zoomMin = 33;
    public float zoomSpeed = 10;

    // key codes to use
    private KeyCode UpKey = KeyCode.W;
    private KeyCode DownKey = KeyCode.S;
    private KeyCode RightKey = KeyCode.D;
    private KeyCode LeftKey = KeyCode.A;
    private KeyCode RotateRightKey = KeyCode.E;
    private KeyCode RotateLeftKey = KeyCode.Q;
    private KeyCode ResetKey = KeyCode.R;
    private string ZoomAxis = "Mouse ScrollWheel";

    // target movement vector
    private Vector2 movement;

    // used for rotation
    private float rotation;
    // starting value of horizontal damping
    private float defaultDamping;

    // used for zooming
    private float zooming;

    // the object to follow (if an object is assigned, it will be followed)
    public Transform follow;

    void Start()
    {
        // set starting position to player's
        SetFollow(player);

        // get default damping value
        defaultDamping = CMVCam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping;
    }

    void Update()
    {
        // if the game is not paused
        if (!Static.paused)
        {
            // MOVEMENT 
            // reset the vector
            movement = Vector2.zero;
            // get input
            if (Input.GetKey(UpKey)) movement += new Vector2(0, 1);
            if (Input.GetKey(DownKey)) movement += new Vector2(0, -1);
            if (Input.GetKey(RightKey)) movement += new Vector2(1, 0);
            if (Input.GetKey(LeftKey)) movement += new Vector2(-1, 0);
            // translate the target
            target.Translate(new Vector3(movement.x, 0, movement.y) * movementSpeed * Time.deltaTime);
            // disable following mode if moved
            if (movement != Vector2.zero)
            {
                SetFollow(null);
            }


            // ROTATION
            // reset
            rotation = 0;
            // get input
            if (Input.GetKey(RotateRightKey)) rotation -= 1;
            if (Input.GetKey(RotateLeftKey)) rotation += 1;
            // set damping to rotationDamping if rotating
            if (rotation != 0)
            {
                CMVCam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = rotationDamping;
            }
            // set default damping value if not rotating
            else
            {
                CMVCam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = defaultDamping;
            }
            // rotate the camera along Y axis
            CMVCam.m_Follow.Rotate(Vector3.up, rotation * rotationSpeed * Time.deltaTime);
            

            // enable following mode if R pressed
            if (Input.GetKeyDown(ResetKey))
            {
                SetFollow(player);
            }


            // zooming - get input and change camera's orthographic size
            zooming = Input.GetAxis(ZoomAxis) * zoomSpeed;
            CMVCam.m_Lens.OrthographicSize = Mathf.Clamp(CMVCam.m_Lens.OrthographicSize - zooming, zoomMax, zoomMin);


            // follow an object if set
            if (follow)
            {
                target.position = follow.position;
            }
        }
    }

    /// <summary>
    /// This resets the target's position to player's
    /// </summary>
    public void SetFollow(Transform newFollow)
    {
        follow = newFollow;
    }
}
