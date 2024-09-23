using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public GameManager gameManager;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    public enum Direction
    {
        Left,
        Right
    }

    private void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (rb.position.y < -1f)
        {
            gameManager.EndGame();
        }
    }

    public void Move(Direction direction)
    {
        if (direction == Direction.Left)
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        if (direction == Direction.Right)
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}
