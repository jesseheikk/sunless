using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    private bool movingRight = true;

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
        // Adjust the front foot position offset and raycast distance based on the character's scale
        float offsetX = 2f; // Adjust horizontal offset
        float raycastDistance = 1f * transform.localScale.y; // Adjust raycast distance

        // Calculate the front foot position based on the direction the character is moving
        Vector2 frontFootPosition = movingRight 
            ? new Vector2(transform.position.x + offsetX, transform.position.y)
            : new Vector2(transform.position.x - offsetX, transform.position.y);

        // Perform the ground check raycast
        RaycastHit2D groundCheck = Physics2D.Raycast(frontFootPosition, Vector2.down, raycastDistance, groundLayer);

        // Check if the raycast hit something
        if (groundCheck.collider == null)
        {
            TurnAround();
        }

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(frontFootPosition, Vector2.down * raycastDistance, Color.red);
    }

    public void TurnAround()
    {
        movingRight = !movingRight;
    }
}
