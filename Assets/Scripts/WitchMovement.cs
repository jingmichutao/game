using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float horizontalBoundaryLeft = -5f;
    public float horizontalBoundaryRight = 5f;
    public float verticalBoundaryTop = 3f;
    public float verticalBoundaryBottom = -3f;

    private bool movingRight = true;
    private bool movingUp = true;

    void Update()
    {
        MoveHorizontally();
        MoveVertically();
    }

    void MoveHorizontally()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= horizontalBoundaryRight)
            {
                movingRight = false;
                FlipHorizontal();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= horizontalBoundaryLeft)
            {
                movingRight = true;
                FlipHorizontal();
            }
        }
    }

    void MoveVertically()
    {
        if (movingUp)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            if (transform.position.y >= verticalBoundaryTop)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            if (transform.position.y <= verticalBoundaryBottom)
            {
                movingUp = true;
            }
        }
    }

    void FlipHorizontal()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
