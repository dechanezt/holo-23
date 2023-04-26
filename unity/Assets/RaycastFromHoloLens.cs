using UnityEngine;
using System.Collections;

public class RaycastFromHoloLens : MonoBehaviour
{
    public Material portalMaterial; // assign the Portal material in the Inspector
    // Déclaration de la distance maximale pour lancer un raycast
    private float maxDistance = 20.0f;

    void Update()
    {
        // Appel de la fonction PerformRaycast à chaque mise à jour du jeu
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        RaycastHit hitInfo;

        // Lance un raycast à partir de la position et de la direction actuelle de cet objet, jusqu'à une distance maximale de maxDistance, et stocke les informations sur la collision dans hitInfo
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance, Physics.DefaultRaycastLayers))
        {
            // Vérifie si la distance entre cet objet et l'objet touché par le raycast est inférieure à 0,1 unité (ce qui correspond à 10 cm dans le jeu)
            if (hitInfo.distance < 0.1f)
            {
                // Affiche les informations sur la collision dans la console de débogage
                print(hitInfo);
                // Calcule une rotation à partir de la normale de la surface touchée par le raycast et de la direction "vers le haut" (y) pour orienter correctement l'objet de jeu instancié
                Quaternion rotation = Quaternion.LookRotation(hitInfo.normal, Vector3.up);

                // tentative de création d'objet
                GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                newObject.transform.position = hitInfo.point;
                newObject.transform.localScale *= 0.1f;
                newObject.GetComponent<SphereCollider>().enabled = false;

                Renderer renderer = newObject.GetComponent<Renderer>();
                renderer.material = portalMaterial;

                // Start a coroutine to gradually scale down the new object before deleting it
                StartCoroutine(ScaleDownAndDestroy(newObject));
            }
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
