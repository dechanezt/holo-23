using UnityEngine;
using System.Collections;

public class RaycastFromHoloLens : MonoBehaviour
{
    private float maxDistance = 20.0f;

    public GameObject prefab;

    void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance, Physics.DefaultRaycastLayers))
        {
            if (hitInfo.distance < 0.1f) // check hit distance is more than 3cm
            {
                print(hitInfo);
                Quaternion rotation = Quaternion.LookRotation(hitInfo.normal, Vector3.up);
                Instantiate(prefab, hitInfo.point, rotation);
            }
        }
    }

}