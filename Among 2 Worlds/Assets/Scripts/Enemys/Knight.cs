using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public enum enemyState { Patrolling, Following, Attacking, Death }
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
    float trackingdistance;
    float playerDistance;
    float knightHeight;
    float knightWidth;
    public float deathdistance;

    public float attackCooldown;
    public float currentAttackCooldown;

    //raycasts
    bool leftGroundRay;
    bool rightGroundRay;

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
        trackingdistance = GetComponent<BoxCollider2D>().size.x;
        //rigidRef.gravityScale = 0;
    }

    void Update()
    {
        Debug.DrawRay(new Vector2(transform.position.x - knightWidth / 2, transform.position.y), Vector2.down * knightHeight / 2, Color.green);
        Debug.DrawRay(new Vector2(transform.position.x + knightWidth / 2, transform.position.y), Vector2.down * knightHeight / 2, Color.green);
        Debug.DrawRay(transform.position, Vector2.left * trackingdistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.right * trackingdistance, Color.red);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        matchState();

        playerDistance = Mathf.Abs(playerRef.transform.position.x - transform.position.x);

        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= 1 * Time.deltaTime;
        }

        if (health <= 0)
        {
            StartCoroutine(death());
        }
        Damage(100);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (knightState == enemyState.Patrolling && collision.tag == "Player")
        {
            knightState = enemyState.Following;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (knightState == enemyState.Following && collision.tag == "Player")
        {
            knightState = enemyState.Patrolling;
        }
    }

    void patrol()
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
                    rigidRef.velocity = Vector2.zero;
                    nextPatrolPoint = patrolPoint.Right;
                    knightDirection = enemyDirection.Right;
                    flipX();
                }
                break;

            case (patrolPoint.Right):
                if (transform.position.x < rightPatrolX)
                {
                    rigidRef.velocity = new Vector2(patrolSpeed, 0);
                }
                if (transform.position.x >= rightPatrolX)
                {
                    rigidRef.velocity = Vector2.zero;
                    nextPatrolPoint = patrolPoint.Left;
                    knightDirection = enemyDirection.Left;
                    flipX();
                }
                break;
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
        if (playerDistance < attackRange && currentAttackCooldown <= 0)
        {
            currentAttackCooldown = attackCooldown;
            knightState = enemyState.Attacking;
            playerRef.GetComponent<Player>().damage(damage);
        }
    }

    public void Damage(int amount)
    {
        if (playerDistance < deathdistance && playerRef.GetComponent<Player>().playerMoveState == Player.moveState.Dashing)
        {
            health = 0;
        }
        if (health <= 0)
        {
            rigidRef.velocity = Vector2.zero;
        }
    }

    void flipX()
    {
        if (knightDirection == enemyDirection.Left)
        {
            transform.localScale = new Vector2(1, 1);
        }
        if (knightDirection == enemyDirection.Right)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void matchState()
    {
        switch (knightState)
        {
            case (enemyState.Patrolling):
                patrol();
                break;

            case (enemyState.Following):
                closeDistance();
                break;

            case (enemyState.Attacking):
                knightState = enemyState.Following;
                break;
        }
    }

    IEnumerator death()
    {
        knightState = enemyState.Death;
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    void updateRays()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x - knightWidth / 2, transform.position.y), Vector2.down, knightHeight * 2, GameManager.GMInstance.platformMask))
        {
            leftGroundRay = true;
        }
        else
        {
            leftGroundRay = false;
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x + knightWidth / 2, transform.position.y), Vector2.down, knightHeight * 2, GameManager.GMInstance.platformMask))
        {
            rightGroundRay = true;
        }
        else
        {
            rightGroundRay = false;
        }
    }

    void attack()
    {
    }
}
