using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private Transform followTarget;

    [SerializeField]
    [Header("Enemy Stats")]
    public int maxHealth;
    public int currentHealth;

    [SerializeField]
    [Header("Follow Variables")]
    public float movementSpeed;
    public float stopDistance;
    public bool isFollowing = false;

    [SerializeField]
    [Header("States")]
    public bool isDead = false;
    public bool canFlip = true;
    public bool facingRight = false;

    [SerializeField]
    [Header("Combat States")]
    public bool detectedPlayer = false;
    public bool combatEnabled = true;
    public bool canBeDamaged = true;

    [SerializeField]
    [Header("Combat Stats")]
    private float inputTimer;
    private bool gotInput;
    private bool isAttacking;
    public float attack1Radius;
    public int attack1Damage;

    [Header("Debug")]
    private bool debugMode;

    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;
    private float lastInputTime = Mathf.NegativeInfinity;

    // HitEffect
    [SerializeField] private FlashEffect simpleFlashEffect;

    void Start()
    {
        currentHealth = maxHealth;
        animator.SetBool("canAttack", combatEnabled);

        // Instantiate Follow Target (Tag)
        followTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (isDead != true)
        {
            CheckDebug();
            CheckFlip();
            CheckPlayerFront();
            CheckAttacks();
        }
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

    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
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

    private void DisableCombat()
    {
        combatEnabled = false;
    }

    private void EnableCombat()
    {
        combatEnabled = true;
    }

    private void EnableCanBeDamaged()
    {
        canBeDamaged = true;
    }

    private void DisableCanBeDamaged()
    {
        canBeDamaged = false;
    }

    private void Damage(int amount)
    {
        if (canBeDamaged == true) 
        {
            currentHealth -= amount;

            // Hit Effects
            //Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            simpleFlashEffect.Flash();
            animator.SetTrigger("damage");

            if (currentHealth <= 0.0f)
            {
                //aliveGO.SetActive(false);
                isDead = true;
                animator.SetBool("isDead", isDead);
            }
        }
        else
        {
            Debug.Log("Can't be damaged");
        }

    }

    private void FinishAnim()
    {
        Destroy(gameObject);
    }

    public void CheckPlayerFront()
    {
        var detectedObjects = Physics2D.OverlapCircle(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);
        if (detectedObjects != null)
        {
            detectedPlayer = true;
            CheckCombatInput();
        }
        else
        {
            detectedPlayer = false;
        }
    }

    public void CheckCombatInput()
    {
        if (combatEnabled)
        {
            if (detectedPlayer == true)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            //Perform attack1
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                animator.SetBool("isAttacking", isAttacking);
            }
        }
        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitbox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.gameObject.SendMessage("Damage", attack1Damage);
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }

    public void beginFollow()
    {
        isFollowing = true;
    }

    public void stopFollow()
    {
        isFollowing = false;
        animator.SetBool("isWalking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

    void FixedUpdate()
    {
        // Move Enemy
        if (isDead != true)
        {
            if (isFollowing == true)
            {
                if (Vector2.Distance(transform.position, followTarget.position) > stopDistance)
                {
                    animator.SetBool("isWalking", true);
                    transform.position = Vector2.MoveTowards(transform.position, followTarget.position, movementSpeed * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }
    }
}
