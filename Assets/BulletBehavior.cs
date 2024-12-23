using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private float _lifeTimer;
    private BulletPool bulletPool;
    private string[] TagsOfBulletReseters = {"Bot", "Player", "Shield"};
    // Start is called before the first frame update
    void Start()
    {
        _lifeTimer = lifeTime;
    }

    public void SetPool(BulletPool pool) {
        bulletPool = pool;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        _lifeTimer -= Time.deltaTime;

        if (_lifeTimer <= 0f) {
            resetBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        for (int i = 0; i < TagsOfBulletReseters.Length; i++) {
            if (other.CompareTag(TagsOfBulletReseters[i])) {
                resetBullet();
                break;
            }
        }
    }

    private void resetBullet() {
        bulletPool.ReturnBullet(gameObject);
        _lifeTimer = lifeTime;
    }
}
