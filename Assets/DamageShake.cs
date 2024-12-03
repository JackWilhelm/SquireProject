using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.25f;
    private Vector2 originalPosition;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            originalPosition = transform.localPosition;
            Debug.Log(originalPosition);
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

        spriteRenderer.color = originalColor;

        transform.localPosition = originalPosition;
    }
}
