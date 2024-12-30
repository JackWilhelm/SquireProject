using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnLaunchers : MonoBehaviour
{
    public GameObject launcherPrefab;  
    public Transform centerPoint;    
    public int numberOfObjects = 10;
    public float radius = 5f;     

    void Start()
    {
        launcherPrefab = Resources.Load<GameObject>("BulletLauncher");
        if (launcherPrefab == null) {
            Debug.LogError("BulletLauncher prefab could not be loaded from Resources!");
        }
        SpawnCircleOfLaunchers();
    }

    void SpawnCircleOfLaunchers()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfObjects;

            float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
            float y = centerPoint.position.y + Mathf.Sin(angle) * radius;

            Vector3 spawnPosition = new Vector3(x, y, centerPoint.position.z);

            GameObject newLauncher = Instantiate(launcherPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
