using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    float moveSpeed = 5f;
    public PolygonCollider2D groundCollider;
    public CircleCollider2D maxDistanceFromBot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 botCenter = maxDistanceFromBot.transform.position;

        float moveX = Input.GetAxis("Horizontal");
         float moveY = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveX, moveY) * moveSpeed;

        playerRigidBody.velocity = move;

        Vector2 newPosition = playerRigidBody.position;
    

        Vector2 fromBotCenter = newPosition - botCenter;
        if (fromBotCenter.magnitude > maxDistanceFromBot.radius) {
            fromBotCenter.Normalize();
            newPosition = botCenter + fromBotCenter * maxDistanceFromBot.radius;
        }

        playerRigidBody.position = newPosition;

        if (!groundCollider.OverlapPoint(newPosition)) {
            ClampPlayerToBounds();
        }
}

    void ClampPlayerToBounds() {
        Vector2 playerPosition = playerRigidBody.position;
        Vector2 closestPoint = groundCollider.ClosestPoint(playerPosition);
        playerRigidBody.position = closestPoint;
    }
}
