using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private Transform followTarget;

    [Header("Statistics")]
    public float movementSpeed;

    [Header("Follow Variables")]
    public float stopDistance;

    [Header("States")]
    public bool isFollowing = false;
    private bool facingRight = false;

    [Header("Debug")]
    public bool debugMode;


    void Start()
    {
        // Instantiate Follow Target (Tag)
        followTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    void Update()
    {
        // Walk Speed Animation Indicator

        //Debug Toggle
        if (debugMode == true)
        {
            Debug.Log("Ally transform.position = " + transform.position.x);
            Debug.Log("target transform.position = " + followTarget.position.x);
        }

        // Flip
        if (transform.position.x > followTarget.position.x && facingRight)
        {
            // Flip NPC left
            Flip();
        }
        else if (transform.position.x < followTarget.position.x && !facingRight)
        {
            // Flip NPC right
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the NPC labelled as facing.
        facingRight = !facingRight;

        // Multiply the NPC's x local scale by -1.
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void FixedUpdate() 
    {
        // Move NPC
        if (isFollowing == true) { 
            if (Vector2.Distance(transform.position, followTarget.position) > stopDistance)
            {
                animator.SetBool("IsWalking", true);
                transform.position = Vector2.MoveTowards(transform.position, followTarget.position, movementSpeed * Time.deltaTime);
            }
            else 
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }
}
