//THIS SCRIPT HANDLES ALL THE MOVEMENT OF THE PLAYER AND ALL THE TRIGGERS WITH THE DATA POINTS
//THE PLAYER CAN LOOK LEFT AND RIGHT WHICH ROTATES THE PLAYER GAME OBJECT AND THE PLAYER CAN LOOK
//UP AND DOWN WHICH ROTATES THE CAMERA OBJECT.

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Movement : MonoBehaviour
{
    public float speed;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;

    private Rigidbody Rb;
    public float oldspeed;
    private Vector2 rotation = Vector2.zero;
    public float Camspeed = 3f;
    public float lookSpeed = 3f;
    public Camera MainCam;
    public float Height;
    public bool crouching = false;
    private float startfov;
    private bool grounded;

    private void Start()
    {
        startfov = MainCam.fieldOfView;
        oldspeed = speed;
        // fogplane.SetActive(false);

        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;
        Rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = oldspeed * 1.7f;
        }
        else
        {
            MainCam.fieldOfView = Mathf.Lerp(MainCam.fieldOfView, startfov, Time.deltaTime * 5);
            speed = oldspeed;
        }

        if (grounded == false)
        {
            // get input
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");

            // clamp the input
            rotation.x = Mathf.Clamp(rotation.x, -30f, 22f);
            // apply rotation of the input only on the Y axis of the player
            transform.eulerAngles = new Vector2(0, rotation.y - 90) * lookSpeed;

            //apply the Z and X axis of the input to the camera.
            MainCam.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //if (targetVelocity != new Vector3(0, 0, 0))
            //{
            //    anim.SetBool("Walking", true);
            //}
            //else
            //{
            //    anim.SetBool("Walking", false);
            //}

            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = Rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            Rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        //  apply gravity manually
        Rb.AddForce(new Vector3(0, -gravity * Rb.mass, 0));
    }
}