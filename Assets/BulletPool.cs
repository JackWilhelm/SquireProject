using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 20;

    private GameObject[] bulletPool;
    // Start is called before the first frame update
    void Start()
    {
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
                return bullet;
            }
        }

        Debug.Log("No bullets left in pool!");
        return null;
    }

    public void ReturnBullet(GameObject bullet) {
        bullet.SetActive(false);
    }
}
