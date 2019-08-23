using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimationController : MonoBehaviour
{

    Knight knightRef;
    public Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        knightRef = GetComponent<Knight>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckKnight();
    }

    void CheckKnight()
    {
        //Patrolling, Following, Attacking 
        bool isPatrolling;
        if (knightRef.knightState == Knight.enemyState.Patrolling)
        {
            isPatrolling = true;
        }
        else
        {
            isPatrolling = false;
        }
        animator.SetBool("isPatrolling", isPatrolling);

        bool isFollowing;
        if (knightRef.knightState == Knight.enemyState.Following)
        {
            isFollowing = true;
        }
        else
        {
            isFollowing = false;
        }
        animator.SetBool("isFollowing", isFollowing);

        bool isAttacking;
        if (knightRef.knightState == Knight.enemyState.Attacking)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        animator.SetBool("isAttacking", isAttacking);

        bool isDead;
        if(knightRef.health <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
        animator.SetBool("isDead", isDead);
    }
}
