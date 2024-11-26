using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.25f;
    public CircleCollider2D botCollider;

    private Vector2 originalPosition;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            originalPosition = transform.localPosition;
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake() {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration) {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPosition + new Vector2(offsetX, 0f);

            elapsedTime += Time.deltaTime;
            spriteRenderer.color = Color.red;
            yield return null;
        }

        spriteRenderer.color = Color.blue;

        transform.localPosition = originalPosition;
    }
}
