using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessfulBlock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float signalDuration = 0.5f;
    private Coroutine blockSignalRoutine;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            if (blockSignalRoutine != null) {
                StopCoroutine(blockSignalRoutine);
            }
            blockSignalRoutine = StartCoroutine(BlockSignal());
        } 
    }

    private IEnumerator BlockSignal() {
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(signalDuration);
        spriteRenderer.color = originalColor;
    }
}
