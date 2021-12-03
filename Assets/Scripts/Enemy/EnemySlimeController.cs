using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private Transform followTarget;

    [SerializeField]
    [Header("Statistics")]
    public float maxHealth;
    public float movementSpeed;

    [Header("Follow Variables")]
    public float stopDistance;

    [Header("States")] 
    public bool isFollowing = false;
    public bool facingRight = false;

    public float currentHealth;
    private GameObject aliveGO;

    [Header("Debug")]
    public bool debugMode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           Debug.Log("Player nearby");
           isFollowing = true;
        }
     }

    void Start()
    {
        currentHealth = maxHealth;

        aliveGO = transform.Find("Alive").gameObject;

        animator = aliveGO.GetComponent<Animator>();
        rb = aliveGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);

        // Instantiate Follow Target (Tag)
        followTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        CheckDebug();
        CheckFlip();
    }

    private void CheckDebug()
    {
        //Debug Toggle
        if (debugMode == true)
        {
            Debug.Log("Enemy transform.position = " + transform.position.x);
            Debug.Log("target transform.position = " + followTarget.position.x);
        }
    }
    
    private void CheckFlip()
    {
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
        // Switch the way the Enemy labelled as facing.
        facingRight = !facingRight;

        // Multiply the Enemy's x local scale by -1.
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Damage(float amount)
    {
        currentHealth -= amount;

        //Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        animator.SetTrigger("damage");

        if (currentHealth <= 0.0f)
        {
            aliveGO.SetActive(false);
            Debug.Log("Dead");
        }
    }

    void FixedUpdate()
    {
        // Move Enemy
        if (isFollowing == true)
        {
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
