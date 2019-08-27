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
    public float movementSpeed;

    /// <summary>
    /// The speed of camera rotation
    /// </summary>
    public float rotationSpeed;

    // key codes to use
    private KeyCode UpKey = KeyCode.W;
    private KeyCode DownKey = KeyCode.S;
    private KeyCode RightKey = KeyCode.D;
    private KeyCode LeftKey = KeyCode.A;
    private KeyCode RotateRightkey = KeyCode.E;
    private KeyCode RotateLeftkey = KeyCode.Q;
    private KeyCode ResetKey = KeyCode.R;
    private string ZoomAxis = "Mouse ScrollWheel";

    // Zoom min, max and speed
    private float zoomMax = 15;
    private float zoomMin = 22;
    private float zoomSpeed = 6;

    // rotation offset max
    private float rotationMax = 30;

    // target movement vector
    private Vector2 movement;

    // used for rotation
    private float rotation;

    // used for zooming
    private float zooming;

    void Start()
    {
        // set starting position to player's
        ResetTarget();
    }

    void Update()
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


        // ROTATION
        // reset
        rotation = 0;
        // get input
        if (Input.GetKey(RotateRightkey)) rotation -= 1;
        if (Input.GetKey(RotateLeftkey)) rotation += 1;
        // rotate the camera along Y axis
        CMVCam.m_Follow.Rotate(Vector3.up, rotation * rotationSpeed * Time.deltaTime);


        // reset the target to player if R pressed
        if (Input.GetKeyDown(ResetKey))
        {
            ResetTarget();
        }


        // zooming - get input and change camera's orthographic size
        zooming = Input.GetAxis(ZoomAxis) * zoomSpeed;
        CMVCam.m_Lens.OrthographicSize = Mathf.Clamp(CMVCam.m_Lens.OrthographicSize - zooming, zoomMax, zoomMin);
    }

    /// <summary>
    /// This resets the target's position to player's
    /// </summary>
    void ResetTarget()
    {
        target.position = player.position;
    } 
}
