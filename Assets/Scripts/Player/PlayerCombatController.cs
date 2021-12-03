using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    [Header("Components")]
    public Button attackButton;
    private Animator anim;

    [SerializeField]
    [Header("States")]
    private bool combatEnabled = false;
    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;

    [SerializeField]
    [Header("Statistics")]
    public float inputTimer;
    public float attack1Radius;
    public int attack1Damage;

    [SerializeField]
    private Transform attack1HitBoxPos;

    [SerializeField]
    private LayerMask whatIsDamageable;

    private float lastInputTime = Mathf.NegativeInfinity;


    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }

    private void Update()
    {
        CheckAttacks();
    }

    public void CheckCombatInput()
    {
        if (combatEnabled)
        {
            gotInput = true;
            lastInputTime = Time.time;
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
                    isFirstAttack = !isFirstAttack;
                    anim.SetBool("attack1", true);
                    anim.SetBool("firstAttack", isFirstAttack);
                    anim.SetBool("isAttacking", isAttacking);
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

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
