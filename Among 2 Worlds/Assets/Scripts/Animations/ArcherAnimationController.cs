using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimationController : MonoBehaviour
{

    Archer archerRef;
    public Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        archerRef = GetComponent<Archer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckArcher();
    }

    void CheckArcher()
    {
        bool LightDimension;   //Checks which Dimension is active right now
        if (GameManager.GMInstance.currentdim == GameManager.dimension.Light)
        {
            LightDimension = true;
        }
        else
        {
            LightDimension = false;
        }
        animator.SetBool("LightDimension", LightDimension);

        //Patrolling, Following, Attacking 
        bool isidle;
        if (archerRef.ArcherState == Archer.enemyState.Idle)
        {
            isidle = true;
        }
        else
        {
            isidle = false;
        }
        animator.SetBool("isidle", isidle);

        bool isAttacking;
        if (archerRef.ArcherState == Archer.enemyState.Attacking)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        animator.SetBool("isAttacking", isAttacking);

        bool isDead;
        if (archerRef.health <= 0)
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
