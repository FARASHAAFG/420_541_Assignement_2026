using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        animator.SetFloat("CharacterSpeed", rb.linearVelocity.magnitude);

        bool grounded = movement.IsGrounded;
        animator.SetBool("IsGrounded", grounded);
        bool isMoving = Input.GetAxisRaw("Vertical") != 0;
        bool wantToRun = Input.GetKey(KeyCode.LeftShift);
        
        animator.SetBool("isRunning", isMoving && wantToRun);

        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetTrigger("doRoll");
        }

        if (grounded)
        {
            animator.ResetTrigger("doDoubleJump");
        }
        else
        {
            //call Flip animation when double jjmp succedds
            if (Input.GetKeyDown(KeyCode.Space) && movement.TryDoubleJump())
            {
                animator.SetTrigger("doDoubleJump");
            }
        }
    }
}