using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float extraGravityMultiplier; // Extra gravity multiplier when in air
    bool readyToJump;



    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float groundCheckRadius; // SphereCast radius for ground check
    public LayerMask whatIsGround;
    [SerializeField] bool grounded;
    public float coyoteTime = 0.2f; // Duration for coyote time
    private float coyoteTimeCounter; // Counter for coyote time

    [Header("Animation")]
    public Animator animator; // Reference to the Animator component

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        // Improved ground check using SphereCast
        grounded = Physics.CheckSphere(transform.position, groundCheckRadius, whatIsGround);

        // Implementing coyote time logic
        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        MyInput();
        SpeedControl();

        // Handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        // Update animation
        UpdateWalkingAnimation();
    }

    private void OnDrawGizmos()
    {
        // Set the color of the Gizmo
        Gizmos.color = Color.yellow;

        // Draw a wire sphere at the ground check position
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }

    private void FixedUpdate()
    {
        MovePlayer();

        // Apply extra gravity when not grounded
        if (!grounded)
        {
            rb.AddForce(Vector3.down * extraGravityMultiplier);
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump input logic with coyote time
        if (Input.GetKey(jumpKey) && readyToJump && coyoteTimeCounter > 0)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void UpdateWalkingAnimation()
    {
        // Check if there is any movement input
        bool isMoving = horizontalInput != 0 || verticalInput != 0;
        animator.SetBool("Walking", isMoving);


    }

    private void MovePlayer()
    {
        
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // On ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        // In air
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // Limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
