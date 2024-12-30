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
    public float lastDashTime {get; private set;} = -100f;
    private bool isDashing = false;
    public float maxDashDistance = 5f;
    public float dashDuration = 0.2f;
    public bool dashMethodMouse = false;
    public float dashDistanceOffset = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(moveX, moveY).normalized;
        DashHandler(moveInput);
        Vector2 newPosition = NormalMovement(moveInput);
        playerRigidBody.position = ClampPlayerToBounds(ClampToBot(newPosition));
    }

    Vector2 NormalMovement(Vector2 moveInput) {
        if (!isDashing) {
            playerRigidBody.velocity = Vector2.Lerp(playerRigidBody.velocity, moveInput * moveSpeed, Time.deltaTime * footGrip);
        }
        return playerRigidBody.position;
    }

    Vector2 ClampToBot(Vector2 newPosition) {
        Vector2 botCenter = maxDistanceFromBot.transform.position;
        Vector2 fromBotCenter = newPosition - botCenter;
        if (fromBotCenter.magnitude > maxDistanceFromBot.radius) {
            fromBotCenter.Normalize();
            newPosition = botCenter + fromBotCenter * maxDistanceFromBot.radius;
        }
        return newPosition;
    }

    Vector2 ClampPlayerToBounds(Vector2 newPosition) {
        if (!groundCollider.OverlapPoint(newPosition)) {
            Vector2 playerPosition = playerRigidBody.position;
            Vector2 closestPoint = groundCollider.ClosestPoint(playerPosition);
            newPosition = closestPoint;
        }
        return newPosition;
    }

    void DashHandler(Vector2 moveInput) {
        if (Input.GetMouseButtonDown(1) && CanDash()) {
            StartCoroutine(Dash(moveInput));
        }
    }

    public bool CanDash() {
        return Time.time >= lastDashTime + dashCooldown;
    }

    private IEnumerator Dash(Vector2 moveDirection) {
        isDashing = true;
        lastDashTime = Time.time;
        float dashDistance = maxDashDistance;

        if (dashMethodMouse) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 directionVector = mousePosition - (Vector2)transform.position;
            moveDirection = directionVector.normalized;
            float distanceToMouse = Vector2.Distance((Vector2)transform.position, mousePosition);
            if (distanceToMouse < maxDashDistance) {
                dashDistance = distanceToMouse - dashDistanceOffset;
            }
            if (dashDistance < 0) {
                dashDistance = 0;
            }
        }
        Vector2 dashVelocity = moveDirection * (dashDistance / dashDuration);
        playerRigidBody.velocity = dashVelocity;
        
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}
