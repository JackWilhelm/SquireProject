using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private float _lifeTimer;
    private BulletPool bulletPool;
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
            bulletPool.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bot")) {
            bulletPool.ReturnBullet(gameObject);
        }
    }
}
