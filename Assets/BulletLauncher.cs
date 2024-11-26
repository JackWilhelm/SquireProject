using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaunch : MonoBehaviour
{
    public BulletPool bulletPool;
    public Transform firePoint;
    public float fireRate = 10f;
    public float fireRateVarianceRange = 2f;

    public float fireRateVarianceBackendLimiter = 0.5f;

    private float _fireCooldown;

    void Start()
    {
        _fireCooldown = fireRate;
        GameObject bulletManager = GameObject.Find("BulletManager");

        if (bulletManager != null)
        {
            bulletPool = bulletManager.GetComponent<BulletPool>();
            Debug.Log("BulletPool successfully assigned to BulletLauncher!");
        }
        else
        {
            Debug.LogError("BulletManager not found in the Scene! BulletLauncher cannot function.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _fireCooldown -= Time.deltaTime * Random.Range(-fireRateVarianceRange * fireRateVarianceBackendLimiter, fireRateVarianceRange);

        if (_fireCooldown <= 0f) {
            Shoot();
            _fireCooldown = fireRate;
        }
    }

    void Shoot() {
        GameObject bullet = bulletPool.GetBullet();
        if (bullet != null) {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
        }
    }
}
