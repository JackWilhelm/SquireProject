using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private GameObject target;

    public Rigidbody2D bodyRigidBody;
    public float moveSpeed = 3f;

    void OnEnable()
    {
        BotTargeting.GiveBotNewTarget += GiveBotNewTarget;
    }

    void OnDisable()
    {
        BotTargeting.GiveBotNewTarget -= GiveBotNewTarget;
    }

    private void GiveBotNewTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            moveTowardsTarget();
        } else {
            bodyRigidBody.velocity = Vector2.zero;
        }
    }

    private void moveTowardsTarget() {
        Vector2 moveInput = new Vector2(target.transform.position.x - bodyRigidBody.position.x, target.transform.position.y - bodyRigidBody.position.y).normalized;
        bodyRigidBody.velocity = moveInput * moveSpeed;
    }
}
