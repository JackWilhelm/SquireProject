using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TetherIndicator : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    private SpriteRenderer spriteRenderer;

    public float maxTetherDistance = 5f;
    public float minTetherDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (object1 == null || object2 == null) return;

        Positioning();
        Scaling();
        Rotating();
    }

    void Positioning() {
        Vector2 midpoint = (object1.position + object2.position) / 2f;
        transform.position = midpoint;
    }

    void Scaling() {
        float distance = Vector2.Distance(object1.position, object2.position);
        transform.localScale = new Vector2(distance, 1/distance);
        Coloring(distance);
    }

    void Rotating() {
        Vector2 direction = (object2.position - object1.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Coloring(float distance) {
        distance = (distance - minTetherDistance) / (maxTetherDistance - minTetherDistance);
        Debug.Log(distance);
        spriteRenderer.color = Color.Lerp(Color.green, Color.red, distance);
    }
}
