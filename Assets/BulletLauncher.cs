using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaunch : MonoBehaviour
{
    public BulletPool bulletPool;
    public Transform firePoint;
    public float fireRate = 10f;
    private float _fireCooldown;
    private float upperLimitOfCooldownReduction = 0.5f;

    void Start()
    {
        _fireCooldown = fireRate - Random.Range(0, fireRate * upperLimitOfCooldownReduction);
        GameObject bulletManager = GameObject.Find("BulletManager");

        if (bulletManager != null) {
            bulletPool = bulletManager.GetComponent<BulletPool>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _fireCooldown -= Time.deltaTime;

        if (_fireCooldown <= 0f) {
            Shoot();
        }
    }

    void Shoot() {
        GameObject bullet = bulletPool.GetBullet();
        if (bullet != null) {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
        }
        _fireCooldown = fireRate - Random.Range(0, fireRate * upperLimitOfCooldownReduction);
    }
}
