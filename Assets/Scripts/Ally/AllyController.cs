using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AllyController : MonoBehaviour
{
    public AIPath aiScript;

    public Rigidbody2D rb;
    public Animator animator;
    private Transform followTarget;

    [SerializeField]
    [Header("AI Path")]
    public bool allyFollow = false;
    public bool allyReachedPlayer;

    void Update()
    {
        allyReachedPlayer = aiScript.reachedEndOfPath;
        //getReachedBool();
        walkAnimation();
        AllyFollow();
    }

    public void getReachedBool()
    {
        if (allyReachedPlayer == true)
        {
            Debug.Log("Player Reached");
        }
        else
        {
            Debug.Log("Player Not Reached");
        }
    }

    public void walkAnimation()
    {
        if (allyReachedPlayer == true)
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }
    }

    private void AllyFollow()
    {
        if (allyFollow == true)
        {
            aiScript.canMove = true;
        }
        else if (allyFollow == false)
        {
            animator.SetBool("IsWalking", false);
            aiScript.canMove = false;
        }
    }

    public void toggleAllyFollow()
    {
        if (allyFollow == true)
        {
            allyFollow = false;
        }
        else if (allyFollow == false)
        {
            allyFollow = true;
        }
    }
}
