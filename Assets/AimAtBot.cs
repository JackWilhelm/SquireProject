using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtBot : MonoBehaviour
{
    public Transform target;
    public float rotationOffset = -90f;

    void Start() {
        GameObject targetObject = GameObject.Find("Bot");

        if (targetObject != null) {
            target = targetObject.GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
