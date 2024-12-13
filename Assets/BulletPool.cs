using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 20;

    private GameObject[] bulletPool;

    public static event Action<GameObject> AlertBot;
    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Bullet");
        if (bulletPrefab == null) {
            Debug.Log("bullet nonexistent");
            return;
        }
        bulletPool = new GameObject[poolSize];
        for (int i = 0; i< poolSize; i++) {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();
            if (bulletBehavior != null) {
                bulletBehavior.SetPool(this);
            }
            bulletPool[i] = bullet;
        }
    }

    public GameObject GetBullet() {
        foreach(GameObject bullet in bulletPool) {
            if (!bullet.activeInHierarchy) {
                bullet.SetActive(true);
                AlertBot?.Invoke(bullet);
                return bullet;
            }
        }
        return null;
    }

    public void ReturnBullet(GameObject bullet) {
        bullet.SetActive(false);
    }
}
