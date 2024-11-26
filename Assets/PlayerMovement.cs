using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    public float moveSpeed = 5f;
    public PolygonCollider2D groundCollider;
    public CircleCollider2D maxDistanceFromBot;
    public int footGrip = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        playerRigidBody.velocity = Vector2.Lerp(playerRigidBody.velocity, new Vector2(moveX, moveY) * moveSpeed, Time.deltaTime * footGrip);
        

        Vector2 newPosition = playerRigidBody.position;
    
        Vector2 botCenter = maxDistanceFromBot.transform.position;
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
