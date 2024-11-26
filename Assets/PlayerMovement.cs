using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    public float moveSpeed = 5f;
    public PolygonCollider2D groundCollider;
    public CircleCollider2D maxDistanceFromBot;
    public float footGrip = 100;
    public float dashCooldown = 2;
    private float lastDashTime = -100f;
    private bool isDashing = false;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public bool dashMethodMouse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(moveX, moveY).normalized;
        if (!isDashing) {
            playerRigidBody.velocity = Vector2.Lerp(playerRigidBody.velocity, moveInput * moveSpeed, Time.deltaTime * footGrip);
        }
        Vector2 newPosition = playerRigidBody.position;

        if (Input.GetMouseButtonDown(1) && CanDash()) {
            Debug.Log("dash");
            StartCoroutine(Dash(moveInput));
        }


    
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

    bool CanDash() {
        return Time.time >= lastDashTime + dashCooldown;
    }

    private IEnumerator Dash(Vector2 moveDirection) {
        isDashing = true;
        lastDashTime = Time.time;

        if (dashMethodMouse) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            moveDirection = (mousePosition - (Vector2)transform.position).normalized;
        }
        Vector2 dashVelocity = moveDirection * dashSpeed;
        playerRigidBody.velocity = dashVelocity;
        

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}
