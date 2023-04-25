using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnter : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Starting detection");

    }

    void HandleCollision(Collision collision)
    {
        Debug.Log("Collision Detected");
        // Get the point of collision
        Vector3 collisionPoint = collision.contacts[0].point;
        Debug.Log("Collision Point: " + collisionPoint.ToString());

        // Instantiate a new cube at the point of collision
        GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newCube.transform.position = collisionPoint;
    }
}
