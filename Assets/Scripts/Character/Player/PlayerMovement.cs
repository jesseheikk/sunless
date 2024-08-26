using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class PlayerMovement : CharacterMovement
{
    [Header("Dash")]
    [SerializeField] float dashSpeedHorizontal = 25f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] TrailRenderer tr;
    [SerializeField] AudioSource dashSound;
    [SerializeField] VisualEffect dashVfx;

    bool canDash = true;
    bool isDashing = false;

    [Header("Jump")]
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float jumpHoldForce = 15f;
    [SerializeField] Transform groundCheck;

    float maxJumpTime = 1f;
    public bool isGrounded;     // Public for camera usage
    bool isJumping;
    float jumpTime;

    [Header("Wall Collision")]
    Vector2 rightRayOriginOffset;
    Vector2 leftRayOriginOffset;

    [SerializeField] AudioSource runSound;

    protected override void Start()
    {
        base.Start();

        // Initialize ray origins for wall collision
        rightRayOriginOffset = new Vector2(0.5f, 0f);
        leftRayOriginOffset = new Vector2(-0.5f, 0f);
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        CheckDash();
        CheckJump();
    }

    void FixedUpdate()
    {
        if (isDashing || !canMove)
        {
            return;
        }

        float moveInput = Input.GetAxis("Horizontal");

        // Check for wall collisions only in the direction of movement
        if (moveInput > 0 && IsWallAhead(Vector2.right))
        {
            moveInput = 0;
        }
        else if (moveInput < 0 && IsWallAhead(Vector2.left))
        {
            moveInput = 0;
        }

        if (moveInput != 0 && isGrounded) {
            if (!runSound.isPlaying) {
                runSound.loop = true;
                runSound.Play();
            }
        } else {
            if (runSound.isPlaying) {
                runSound.Stop();
            }
        }

        Move(moveInput);
    }

    void CheckJump()
    {
        // Only allow jump if player's feet are touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
                jumpTime = 0f;
            }
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            // Continue applying upward force while the jump button is held
            // to give player more control of the jump
            rb.AddForce(Vector2.up * jumpHoldForce * Time.deltaTime, ForceMode2D.Impulse);

            // Limit the total jump time
            if (jumpTime < maxJumpTime)
            {
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            isJumping = false;
        }
    }

    void CheckDash()
    {
        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        if (dashSound)
        {
            dashSound.time = 0.2f;
            dashSound.Play();
        }

        GhostTrail trail = GetComponent<GhostTrail>();
        trail?.StartEmit();
        canDash = false;
        isDashing = true;

        // Save previous gravity and set it to 0
        // to prevent player from falling during the dash
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Apply velocity during the dash duration
        rb.velocity = new Vector2(transform.localScale.x * dashSpeedHorizontal, 0f);
        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
        trail?.StopEmit();

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
