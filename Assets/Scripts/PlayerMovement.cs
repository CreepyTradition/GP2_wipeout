using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;

    public Transform cameraTransform;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float groundDistance = 0.3f;

    private Rigidbody rb;

    private Vector2 moveInput;

    private bool isGrounded;

    private int jumpCount;

    public int maxJumps = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GroundCheck();

        Move();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundLayer
        );

        if (isGrounded)
        {
            jumpCount = 1;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                jumpForce,
                rb.linearVelocity.z
            );

            jumpCount++;
        }
    }

    void Move()
    {
        Vector3 direction =
            new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle =
                Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                cameraTransform.eulerAngles.y;

            Vector3 moveDir =
                Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.forward = moveDir;

            rb.linearVelocity = new Vector3(
                moveDir.x * moveSpeed,
                rb.linearVelocity.y,
                moveDir.z * moveSpeed
            );
        }
        else
        {
            rb.linearVelocity = new Vector3(
                0f,
                rb.linearVelocity.y,
                0f
            );
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundDistance
        );
    }
}