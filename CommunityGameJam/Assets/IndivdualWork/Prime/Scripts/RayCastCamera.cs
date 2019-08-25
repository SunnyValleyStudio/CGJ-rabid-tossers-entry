using UnityEngine;

public class RayCastCamera : MonoBehaviour
{
    public Camera cam;

    private void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.transform.gameObject.GetComponentInParent<IInterActable>() != null)
                {
                    hit.transform.gameObject.GetComponentInParent<IInterActable>().Interacted();
                }
                else
                {
                    Debug.Log("Doesnt have the interactable script or is not on the same object as the collider");
                }
            }
        }
    }
}