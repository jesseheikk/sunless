using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    bool movingRight = true;

    void Update()
    {
        CheckForWall();
        CheckForEdge();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            float direction = movingRight ? 1 : -1;
            Move(direction);
        }
    }

    void CheckForWall()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        if (IsWallAhead(direction))
        {
            TurnAround();
        }
    }

    void CheckForEdge()
    {
        float offsetX = 2f;
        float raycastDistance = 1f * transform.localScale.y;

        // Calculate the front foot position based on the direction the character is moving
        Vector2 frontFootPosition = movingRight 
            ? new Vector2(transform.position.x + offsetX, transform.position.y)
            : new Vector2(transform.position.x - offsetX, transform.position.y);

        RaycastHit2D groundCheck = Physics2D.Raycast(frontFootPosition, Vector2.down, raycastDistance, groundLayer);
        if (groundCheck.collider == null)
        {
            TurnAround();
        }
    }

    public void TurnAround()
    {
        movingRight = !movingRight;
    }
}
