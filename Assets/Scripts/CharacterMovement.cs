using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
[Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float turnSpeed = 150f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float doubleJumpForce = 4f;

    [Header("Ground Check Settings")]
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float turnInput;
    private bool isGrounded;
    private bool jumpRequested = false;
    private bool doubleJumpRequested = false;
    private bool hasDoubleJumped = false;
    private bool isRunning = false;

    public bool IsGrounded => isGrounded;

    private PlayerPowerUps powerUps;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        powerUps = GetComponent<PlayerPowerUps>();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position + transform.up*groundDistance/2, -transform.up, groundDistance, groundMask);

        if (isGrounded)
        {
            hasDoubleJumped = false;
        }

        turnInput = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
       
        isRunning = Input.GetKey(KeyCode.LeftShift) && moveZ > 0;
        transform.Rotate(0f, turnInput * turnSpeed * Time.deltaTime, 0f);

        moveDirection = transform.forward * moveZ;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
 
        if (jumpRequested)
        {
            Jump(jumpForce);
            jumpRequested = false;
        }
 
        if (doubleJumpRequested)
        {
            Jump(doubleJumpForce);
            doubleJumpRequested = false;
        }
    }


    private void MovePlayer()
    {

        float currentSpeed = isRunning ? moveSpeed * 2f : moveSpeed;

        if (powerUps != null && powerUps.hasSpeedBoost)
        {
            currentSpeed *= 1.5f; // Increases movement speed
        }

        Vector3 targetVelocity = moveDirection * moveSpeed;

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    private void Jump(float force)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * force, ForceMode.VelocityChange);
    }

    public bool TryDoubleJump()
    {
        if (powerUps == null || !powerUps.hasJumpBoost) return false;

        if (isGrounded || hasDoubleJumped) return false;
 
        doubleJumpRequested = true;
        hasDoubleJumped = true;
        return true;
    }
}