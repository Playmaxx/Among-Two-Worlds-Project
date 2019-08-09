using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public enum enemyState { Patrolling, Following, Attacking }
    public enemyState knightState;

    enum patrolPoint { Left, Right }
    [SerializeField]
    patrolPoint nextPatrolPoint;

    public enum enemyDirection { Left, Right }
    public enemyDirection knightDirection;

    public float leftPatrolX;
    public float rightPatrolX;
    public int attackRange;
    public int patrolSpeed;
    public int attackSpeed;
    public int health;
    public int damage;
    public float trackingdistance;
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
        knightDirection = enemyDirection.Left;
        knightHeight = GetComponent<CapsuleCollider2D>().size.y;
        knightWidth = GetComponent<CapsuleCollider2D>().size.x;
        //rigidRef.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        matchState();
        trackPlayer();

        playerDistance = Mathf.Abs(playerRef.transform.position.x - transform.position.x);

        Debug.DrawRay(new Vector2(transform.position.x - knightWidth / 2, transform.position.y), Vector2.down * knightHeight / 2, Color.green);
        Debug.DrawRay(new Vector2(transform.position.x + knightWidth / 2, transform.position.y), Vector2.down * knightHeight / 2, Color.green);

        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + knightHeight / 2), Vector2.left * trackingdistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * trackingdistance, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - knightHeight / 2), Vector2.left * trackingdistance, Color.red);
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

    public void Damage(int amount)
    {
        health -= amount;
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

    void trackPlayer()
    {
        Vector2 topVector = new Vector2(transform.position.x, transform.position.y + knightHeight / 2);
        Vector2 middleVector = transform.position;
        Vector2 bottomVector = new Vector2(transform.position.x, transform.position.y - knightHeight / 2);

        if (knightState == enemyState.Patrolling)
        {
            switch (knightDirection)
            {
                case (enemyDirection.Left):
                    if (Physics2D.Raycast(topVector, Vector2.right, trackingdistance, GameManager.GMInstance.playerMask))
                    {
                        if (!Physics2D.Raycast(topVector, Vector2.right, trackingdistance, GameManager.GMInstance.platformMask))
                        {
                            knightState = enemyState.Following;
                        }
                    }
                    if (Physics2D.Raycast(middleVector, Vector2.right, trackingdistance, GameManager.GMInstance.playerMask))
                    {
                        if (!Physics2D.Raycast(middleVector, Vector2.right, trackingdistance, GameManager.GMInstance.platformMask))
                        {
                            knightState = enemyState.Following;
                        }
                    }
                    if (Physics2D.Raycast(middleVector, Vector2.right, trackingdistance, GameManager.GMInstance.playerMask))
                    {
                        if (!Physics2D.Raycast(middleVector, Vector2.right, trackingdistance, GameManager.GMInstance.platformMask))
                        {
                            knightState = enemyState.Following;
                        }
                    }
                    break;

                case (enemyDirection.Right):
                    if (Physics2D.Raycast(topVector, Vector2.right, trackingdistance, GameManager.GMInstance.playerMask))
                    {
                        if (!Physics2D.Raycast(topVector, Vector2.right, trackingdistance, GameManager.GMInstance.platformMask))
                        {
                            knightState = enemyState.Following;
                        }
                    }
                    if (Physics2D.Raycast(middleVector, Vector2.right, trackingdistance, GameManager.GMInstance.playerMask))
                    {
                        if (!Physics2D.Raycast(middleVector, Vector2.right, trackingdistance, GameManager.GMInstance.platformMask))
                        {
                            knightState = enemyState.Following;
                        }
                    }
                    if (Physics2D.Raycast(middleVector, Vector2.right, trackingdistance, GameManager.GMInstance.playerMask))
                    {
                        if (!Physics2D.Raycast(middleVector, Vector2.right, trackingdistance, GameManager.GMInstance.platformMask))
                        {
                            knightState = enemyState.Following;
                        }
                    }
                    break;
            }
        }

        if (knightState == enemyState.Following)
        {
            
        }
    }
}
