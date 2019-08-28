using UnityEngine;

public class TargetMovement : MonoBehaviour
{
[SerializeField]
private Vector3 bounds;
[SerializeField]
private float moveSpeed = 10.0f;
[SerializeField]
private float turnSpeed = 3.0f;
[SerializeField]
private float targetPointTolerance = 5.0f;


private Vector3 initialPosition;
private Vector3 nextMovementPoint;
private Vector3 targetPosition;

private bool firstTimeVelocitySave = false;
private Vector3 savedVelocity;
public Rigidbody rb;
[SerializeField]
private float timer = 0.0f;
public float bobbingSpeed = 0.18f;
public float bobbingAmount = 0.2f;
private float midpoint = 2.0f;



private void Awake()
{


        rb.velocity = new Vector3(0, 5, 0);



        initialPosition = transform.position;
        CalculateNextMovementPoint();
}

private void Update ()
{
        float waveslice = 0.0f;
        Vector3 cSharpConversion = transform.localPosition;
        waveslice = Mathf.Sin(timer);
        timer = timer + bobbingSpeed;
        if (timer > Mathf.PI * 2)
        {
                timer = timer - (Mathf.PI * 2);
        }

        if (waveslice != 0)
        {
                float translateChange = waveslice * bobbingAmount;
                cSharpConversion.y = midpoint + translateChange;
        }
        transform.localPosition = cSharpConversion;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), turnSpeed * Time.deltaTime);


        if(Vector3.Distance(nextMovementPoint, transform.position) <= targetPointTolerance)
        {
                CalculateNextMovementPoint();
        }
}


private void CalculateNextMovementPoint()
{
        float posX = Random.Range(initialPosition.x - bounds.x, initialPosition.x + bounds.x);
        float posY = Random.Range(initialPosition.y - bounds.y, initialPosition.y + bounds.y);
        float posZ = Random.Range(initialPosition.z - bounds.z, initialPosition.z + bounds.z);
        targetPosition.x = posX;
        targetPosition.y = posY;
        targetPosition.z = posZ;
        nextMovementPoint = initialPosition + targetPosition;
}
}
