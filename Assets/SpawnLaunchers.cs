using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaunchers : MonoBehaviour
{
    public GameObject prefabToSpawn;  
    public Transform centerPoint;    
    public int numberOfObjects = 10;
    public float radius = 5f;       

    void Start()
    {
        SpawnCircle();
    }

    void SpawnCircle()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfObjects;

            float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
            float y = centerPoint.position.y + Mathf.Sin(angle) * radius;

            Vector3 spawnPosition = new Vector3(x, y, centerPoint.position.z);

            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
