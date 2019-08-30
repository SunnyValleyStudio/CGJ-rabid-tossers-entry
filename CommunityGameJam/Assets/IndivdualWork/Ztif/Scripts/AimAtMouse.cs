using UnityEngine;
using System.Collections;

public class AimAtMouse : MonoBehaviour
{
public Camera cam;
public GameObject slowaimTarget;
public GameObject aimTarget;
private SphereCollider aimCollider;
public float movementTime;
public float SlewRate = 2.0f;

public GameObject[] closeTargets;

Transform looker;
public float speed = 1.0f;
public float targetRadius = 100f;
int layerMask;

void Start()
{

        aimCollider = aimTarget.GetComponent<SphereCollider>();

        //  Cursor.visible = false;
        looker = new GameObject().transform;

        int layerMask = LayerMask.GetMask("Ground", "Road");
        Debug.Log("LayerMask:" +layerMask);

        movementTime = 0f;
        MoveForSeconds(3f);
}

void FixedUpdate ()
{
//RAYCAST FROM CAMERA TO MOUSE POSITION -> ON GROUND PLANE

        //  Vector3 worldpos = cam.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)); //used?

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        //BUG Have to manually enter LayerMask 8192 below...
        if (Physics.Raycast(ray, out hit, 1000f, 8192)) {
//POSITION AIMTARGET AT RAYCAST HITPOINT
                aimTarget.transform.position = hit.point;
//CREATE TargetPosition VARIABLE WHICH IS ...
                Vector3 aimTargetPosition = new Vector3(aimTarget.transform.position.x, aimTarget.transform.position.y, aimTarget.transform.position.z);
                Vector3 slowaimTargetPosition = new Vector3(slowaimTarget.transform.position.x, aimTarget.transform.position.y, slowaimTarget.transform.position.z);

                /*      Vector3 targetPostition = new Vector3(   aimTarget.transform.position.x,
                                                               this.transform.position.y,
                                                               aimTarget.transform.position.z );
                 */

                //if(Time.time < movementTime) {
                slowaimTarget.transform.position = Vector3.MoveTowards(slowaimTargetPosition, aimTargetPosition, Time.deltaTime * speed);
                //  }
                //    float step =  speed * Time.deltaTime;
                //  slowaimTarget.transform.position = Vector3.MoveTowards(aimTargetPosition, slowaimTargetPosition, step);

                /*      if (Vector3.Distance(slowaimTargetPosition, aimTargetPosition) < targetRadius) {
                              Debug.Log("targetRadius:" + targetRadius);
                              //  slowaimTarget.transform.LookAt(aimTarget.transform.position);
                              //    slowaimTarget.transform.Translate(slowaimTargetPosition, aimTargetPosition, speed * Time.deltaTime);
                      } */

        }

        //  targetPosition = GetClosestObject("targetObject");
        //  aimTarget.transform.position = worldpos;

        //  looker.LookAt( aimTarget.transform.position);
        //  Barrel.rotation = Quaternion.Lerp(
        //        Barrel.rotation, looker.rotation, SlewRate * Time.deltaTime);

        //    Ui.rotation = Quaternion.Lerp(
        //  Ui.rotation, looker.rotation, SlewRate * Time.deltaTime);
        ///// Raycast from Camera to


}
/*
   void GetClosestObject(string tag)
   {
        var objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject;
        foreach(GameObject obj in objectsWithTag) {
                if(!closestObject) {
                        closestObject = obj;
                }
                //Compares distances
                if(Vector3.Distance(transform.position, obj.transform.position) <= Vector3.Distance(transform.position, closestObject.transform.position)) {
                        closestObject = obj;
                }
        }
        //return closestObject;
   }
 */

void OnTriggerEnter(Collider col)
{
        if(col.gameObject.tag == "Player")
        {

                Debug.Log ("Player in Range!");
        }
}


void MoveForSeconds(float seconds) {
        movementTime = Time.time + seconds;
}



private void OnDrawGizmos()
{
        //Gizmos.DrawWireaimTarget(looker.transform, 1.0f);

        Gizmos.color = Color.yellow;
        //  Gizmos.DrawSphere(slowaimTarget.transform.position, targetRadius);
}

}
