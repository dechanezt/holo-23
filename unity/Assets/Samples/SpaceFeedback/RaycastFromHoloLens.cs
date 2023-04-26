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
            print(hitInfo);
            Quaternion rotation = Quaternion.LookRotation(hitInfo.normal, Vector3.up);
            Instantiate(prefab, hitInfo.point, rotation);
        }
    }

    IEnumerator ScaleDownAndDestroy(GameObject objectToDestroy)
    {
        float scaleSpeed = 2f; // adjust this value to change the speed of scaling down
        float scaleDuration = 1f; // adjust this value to change the duration of scaling down

        yield return new WaitForSeconds(10f); // wait for 10 seconds before starting the scaling down process

        // Gradually scale down the object over the set duration
        for (float t = 0; t < scaleDuration; t += Time.deltaTime)
        {
            float scaleFactor = Mathf.Lerp(1f, 0f, t / scaleDuration);
            objectToDestroy.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            yield return null;
        }

        // Set the scale to exactly 0 to avoid floating-point precision issues
        objectToDestroy.transform.localScale = Vector3.zero;

        // Destroy the object
        Destroy(objectToDestroy);
    }
}
