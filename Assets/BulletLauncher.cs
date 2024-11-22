using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaunch : MonoBehaviour
{
    public BulletPool bulletPool;
    public Transform firePoint;
    public float fireRate = 0.2f;

    private float _fireCooldown;

    // Update is called once per frame
    void Update()
    {
        _fireCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && _fireCooldown <= 0f) {
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
