using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnter : MonoBehaviour
{
    public Material portalMaterial; // assign the Portal material in the Inspector

    void Start()
    {
        Debug.Log("Starting detection");

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            // Collision detected with VR controller, create new object at point of collision
            Debug.Log("New cube");

            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObject.transform.position = collisionPoint;
            newObject.transform.localScale *= 0.1f; // make the cube 100 times smaller

            // Assign the Portal material to the new cube object
            Renderer renderer = newObject.GetComponent<Renderer>();
            renderer.material = portalMaterial;

            // Start a coroutine to gradually scale down the new object before deleting it
            StartCoroutine(ScaleDownAndDestroy(newObject));
        }
    }

    IEnumerator ScaleDownAndDestroy(GameObject objectToDestroy)
    {
        float scaleSpeed = 2f; // adjust this value to change the speed of scaling down
        float scaleDuration = 1f; // adjust this value to change the duration of scaling down

        yield return new WaitForSeconds(10f); // wait for 5 seconds before starting the scaling down process

        // Gradually scale down the object over the set duration
        for (float t = 0; t < scaleDuration; t += Time.deltaTime)
        {
            float scaleFactor = Mathf.Lerp(0.1f, 0f, t / scaleDuration);
            objectToDestroy.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            yield return null;
        }

        // Set the scale to exactly 0 to avoid floating-point precision issues
        objectToDestroy.transform.localScale = Vector3.zero;

        // Destroy the object
        Destroy(objectToDestroy);
    }


}
