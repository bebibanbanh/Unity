using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public float minX = -5f;
    public float maxX = 5f;
    public float minZ = -5f;
    public float maxZ = 5f;
    public GameObject player;

    private List<GameObject> spheres = new List<GameObject>();

    void Start()
    {
        SpawnSpheres();
    }

    public void SpawnSpheres()
    {
        // Create a list of possible colors
        List<Color> colors = new List<Color> { Color.red, Color.yellow, Color.blue };

        for (int i = 0; i < 3; i++)
        {
            // Generate a random position on the plane
            Vector3 position = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));

            // Instantiate the sphere object with a random unique color
            GameObject sphere = Instantiate(spherePrefab, position, Quaternion.identity);
            Renderer renderer = sphere.GetComponent<Renderer>();
            int colorIndex = Random.Range(0, colors.Count);
            renderer.material.color = colors[colorIndex];
            colors.RemoveAt(colorIndex);

            // Add a collider component to the sphere
            sphere.AddComponent<SphereCollider>();

            // Set the sphere's parent to this object
            sphere.transform.parent = transform;

            // Add the sphere to the list of spheres
            spheres.Add(sphere);
        }
    }

    public void DestroySpheres()
    {
        foreach (GameObject sphere in spheres)
        {
            Destroy(sphere);
        }
        spheres.Clear();
    }
}