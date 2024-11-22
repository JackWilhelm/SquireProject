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

    // Take player input
    float moveX = Input.GetAxis("Horizontal");
    float moveY = Input.GetAxis("Vertical");

    Vector2 move = new Vector2(moveX, moveY) * moveSpeed;

    // Apply movement to the player's Rigidbody
    playerRigidBody.velocity = move;

    // Check if player's position is inside bounds
    Vector2 newPosition = playerRigidBody.position;
    

    // Clamp based on distance from bot center
    Vector2 fromBotCenter = newPosition - botCenter;
    if (fromBotCenter.magnitude > maxDistanceFromBot.radius) {
        fromBotCenter.Normalize();
        newPosition = botCenter + fromBotCenter * maxDistanceFromBot.radius;
    }

    // Update the player's clamped position
    playerRigidBody.position = newPosition;

    if (!groundCollider.OverlapPoint(newPosition)) {
        // If out of bounds, stop the player from leaving
        ClampPlayerToBounds();
    }
}

// Function to clamp the player within PolygonCollider2D bounds
void ClampPlayerToBounds() {
    Vector2 playerPosition = playerRigidBody.position;

    // Find the closest point on the collider to the player's current position
    Vector2 closestPoint = groundCollider.ClosestPoint(playerPosition);

    // Set the player's position to the closest point inside the bounds
    playerRigidBody.position = closestPoint;
}

}
