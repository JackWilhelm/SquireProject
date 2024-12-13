using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaunch : MonoBehaviour
{
    public BulletPool bulletPool;
    public Transform firePoint;
    public float fireRate = 10f;
    private float _fireCooldown;

    void Start()
    {
        _fireCooldown = fireRate - Random.Range(0, fireRate/2);
        GameObject bulletManager = GameObject.Find("BulletManager");

        if (bulletManager != null)
        {
            bulletPool = bulletManager.GetComponent<BulletPool>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _fireCooldown -= Time.deltaTime;

        if (_fireCooldown <= 0f) {
            Shoot();
            _fireCooldown = fireRate - Random.Range(0, fireRate/2);
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
