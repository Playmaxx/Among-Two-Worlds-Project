using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    enum enemyState { Patrolling, Following, Attacking }
    enemyState knightState;

    enum patrolPoint { Left, Right }
    patrolPoint nextPatrolPoint;

    enum enemyDirection { Left, Right }
    enemyDirection knightDirection;

    public float leftPatrolX;
    public float rightPatrolX;
    public int attackRange;
    public int patrolSpeed;
    public int attackSpeed;
    public int health;
    public int damage;
    float playerDistance;
    float knightHeight;
    float knightWidth;

    GameObject playerRef;
    Rigidbody2D rigidRef;

    void Awake()
    {
        playerRef = GameObject.FindWithTag("Player");
        rigidRef = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        knightState = enemyState.Patrolling;
        knightHeight = GetComponent<CapsuleCollider2D>().size.y;
        knightWidth = GetComponent<CapsuleCollider2D>().size.x;
        //rigidRef.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        matchState();

        playerDistance = Mathf.Abs(playerRef.transform.position.x - transform.position.x);

        Debug.DrawRay(new Vector2(transform.position.x - knightWidth / 2, transform.position.y), Vector2.down * knightHeight / 2, Color.green);
        Debug.DrawRay(new Vector2(transform.position.x + knightWidth / 2, transform.position.y), Vector2.down * knightHeight / 2, Color.green);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (knightDirection)
            {
                case (enemyDirection.Right):
                    Vector2 start = rigidRef.transform.position;
                    Vector2 direction = Vector2.right;
                    float distance = GetComponent<BoxCollider2D>().size.x;
                    RaycastHit2D Hit;
                    //if (Physics2D.Raycast(start, direction, distance,out Hit))
                    //{
                    //
                    //}
                    break;

                case (enemyDirection.Left):

                    break;
            }
            knightState = enemyState.Following;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            knightState = enemyState.Patrolling;
        }
    }

    void patrol()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x - knightWidth / 2, transform.position.y), Vector2.down, knightHeight / 2, GameManager.GMInstance.platformMask))
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x + knightWidth / 2, transform.position.y), Vector2.down, knightHeight / 2, GameManager.GMInstance.platformMask))
            {
                switch (nextPatrolPoint)
                {
                    case (patrolPoint.Left):
                        if (transform.position.x > leftPatrolX)
                        {
                            rigidRef.velocity = new Vector2(-patrolSpeed, 0);
                        }
                        if (transform.position.x <= leftPatrolX)
                        {
                            nextPatrolPoint = patrolPoint.Right;
                        }
                        break;

                    case (patrolPoint.Right):
                        if (transform.position.x > leftPatrolX)
                        {
                            rigidRef.velocity = new Vector2(-patrolSpeed, 0);
                        }
                        if (transform.position.x <= leftPatrolX)
                        {
                            nextPatrolPoint = patrolPoint.Right;
                        }
                        break;
                }
            }
        }
    }

    void closeDistance()
    {
        if (playerRef.transform.position.x < transform.position.x && playerDistance > attackRange)
        {
            rigidRef.velocity = new Vector2(-attackSpeed, 0);
        }
        if (playerRef.transform.position.x > transform.position.x && playerDistance > attackRange)
        {
            rigidRef.velocity = new Vector2(attackSpeed, 0);
        }
        if (playerDistance < attackRange)
        {
            knightState = enemyState.Attacking;
        }
    }

    void Damage(int amount)
    {
        health -= amount;
    }

    void Heal(int amount)
    {
        health += amount;
    }

    void matchState()
    {
        if (rigidRef.velocity.x > 0)
        {
            knightDirection = enemyDirection.Right;
        }
        else
        {
            knightDirection = enemyDirection.Left;
        }

        switch (knightState)
        {
            case (enemyState.Patrolling):
                patrol();
                break;

            case (enemyState.Following):
                closeDistance();
                break;

            case (enemyState.Attacking):
                playerRef.GetComponent<Player>().damage(damage);
                break;
        }
    }
}
