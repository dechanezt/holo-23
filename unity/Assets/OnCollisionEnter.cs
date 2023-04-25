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
        }
    }
}
