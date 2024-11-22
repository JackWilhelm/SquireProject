using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    float moveSpeed = 5f;
    public BoxCollider2D groundCollider;
    public CircleCollider2D maxDistanceFromBot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        Bounds groundBounds = groundCollider.bounds;

        Vector2 botCenter = maxDistanceFromBot.transform.position;

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveX, moveY) * moveSpeed;

        playerRigidBody.velocity = move;

        Vector2 clampedPosition = playerRigidBody.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, groundBounds.min.x, groundBounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, groundBounds.min.y, groundBounds.max.y);

        Vector2 fromBotCenter = clampedPosition - botCenter;
        if (fromBotCenter.magnitude > maxDistanceFromBot.radius) {
            fromBotCenter.Normalize();
            clampedPosition = botCenter + fromBotCenter * maxDistanceFromBot.radius;
        }

        playerRigidBody.position = clampedPosition;
    }

}
