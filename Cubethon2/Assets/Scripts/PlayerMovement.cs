using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public GameManager gameManager;
    public Collectable collectable;
    private PlayerCollision collision;
    public Score score;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    public float maxSpeed = 10f;
    private bool reverseControls = false;
    private bool controlsEnabled = true;
    private bool reverseGravity = false;
    public enum Direction
    {
        Left,
        Right
    }

    private void Awake()
    {
        collision = GetComponent<PlayerCollision>();    
    }

    private void OnEnable()
    {
        collectable.OnPickup += ReverseControls;
        collision.OnCrash += UnbindRotation;
        score.OnScoreMilestone += EnableReverseGravity;

    }
    private void OnDisable()
    {
        collectable.OnPickup -= ReverseControls;
        collision.OnCrash -= UnbindRotation;
        score.OnScoreMilestone -= EnableReverseGravity;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (!reverseControls)
            rb.AddForce(0, -1 * 10, 0, ForceMode.Acceleration);

        if (reverseGravity)
            rb.AddForce(0, 1 * 210, 0, ForceMode.Acceleration);


        if (rb.position.y < -1f || rb.position.y > 86f)
        {
            gameManager.EndGame();
        }

    }

    void UnbindRotation()
    {
        rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
        rb.AddTorque(Vector3.one * 10, ForceMode.Impulse);
    }

    void ReverseControls()
    {
        reverseControls = true;
        StartCoroutine(PauseControls(2.5f));
        rb.velocity = new Vector3(0, 0, rb.velocity.z);
    }

    IEnumerator PauseControls(float seconds)
    {
        controlsEnabled = false;
        yield return new WaitForSeconds(seconds);
        controlsEnabled = true;
    }

    void EnableReverseGravity()
    {
        reverseGravity = true;
    }

    public void Move(Direction direction)
    {
        if (!controlsEnabled) return;
        if (reverseControls)
        {
            if (direction == Direction.Left)
                direction = Direction.Right;
            else if (direction == Direction.Right)
                direction = Direction.Left;
        }

        if (direction == Direction.Left)
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        if (direction == Direction.Right)
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}
