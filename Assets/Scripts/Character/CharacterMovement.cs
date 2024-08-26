using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterBase
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] float wallCheckDistance = 1f;

    protected bool canMove = true;

    protected override void Start()
    {
        base.Start();
    }

    public void DisableMovement()
    {
        canMove = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetBool("Running", false);
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    protected void Move(float direction)
    {
        rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
        bool isRunning = Mathf.Abs(direction) > 0.01f;
        animator.SetBool("Running", isRunning);
        FaceMovingDirection(direction);
    }

    void FaceMovingDirection(float direction)
    {
        if (direction == 0)
        {
            return;
        }

        float xScaleMultiplier = direction > 0 ? 1 : -1;
        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * xScaleMultiplier,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    protected bool IsWallAhead(Vector2 direction)
    {
        float rayStartOffset = 0.1f * Mathf.Max(transform.localScale.x, transform.localScale.y);
        Vector2 rayStart = (Vector2)transform.position + direction * rayStartOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, direction, wallCheckDistance, groundLayer);

        return hit.collider != null;
    }
}