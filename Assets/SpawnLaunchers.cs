using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaunchers : MonoBehaviour
{
    public GameObject prefabToSpawn;  // The prefab to spawn
    public Transform centerPoint;    // Central point around which prefabs are spawned
    public int numberOfObjects = 10; // Number of prefabs to spawn in the circle
    public float radius = 5f;        // Radius of the circle

    void Start()
    {
        SpawnCircle();
    }

    void SpawnCircle()
    {
        // Loop through and calculate position for each prefab instance
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Calculate angle for evenly spaced objects (in radians)
            float angle = i * Mathf.PI * 2f / numberOfObjects;

            // Calculate position using sine and cosine (X, Y in a 2D plane, Z for 3D)
            float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
            float y = centerPoint.position.y + Mathf.Sin(angle) * radius;

            // Create a position vector
            Vector3 spawnPosition = new Vector3(x, y, centerPoint.position.z);

            // Spawn the prefab at the calculated position with no rotation
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
