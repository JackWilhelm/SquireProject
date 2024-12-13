using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private Queue<GameObject> targetQueue = new Queue<GameObject>();
    private GameObject target;

    public Rigidbody2D bodyRigidBody;
    public float moveSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable() // Use OnEnable to be robust against subscriptions
    {
        BulletPool.AlertBot += AlertBot;
    }

    void OnDisable() // Always unsubscribe when the object is destroyed or disabled
    {
        BulletPool.AlertBot -= AlertBot;
    }

    private void AlertBot(GameObject newTarget)
    {
        targetQueue.Enqueue(newTarget);
    }


    // Update is called once per frame
    void Update()
    {
        if (target == null && targetQueue.Count > 0) {
            target = targetQueue.Peek();
        }
        if (target != null) {
            if (!target.activeInHierarchy) {
                targetQueue.Dequeue();
                if (targetQueue.Count > 0) {
                    target = targetQueue.Peek();
                } else {
                    target = null;
                    bodyRigidBody.velocity = Vector2.zero;
                }
            } else { 
                moveTowardsTarget();
            }
        }
    }

    private void moveTowardsTarget() {
        Vector2 moveInput = new Vector2(target.transform.position.x - bodyRigidBody.position.x, target.transform.position.y - bodyRigidBody.position.y).normalized;
        bodyRigidBody.velocity = moveInput * moveSpeed;
    }
}
