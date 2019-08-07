using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    enum enemyState { Patrolling, Following, Attacking}
    enemyState knightMoveState;

    public float leftPatrolX;
    public float rightPatrolX;
    public float attackRange;
    public float patrolSpeed;
    public float attackSpeed;
    public float health;
    public float damage;
    float playerDistance;

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

    }

    // Update is called once per frame
    void Update()
    {
        closeDistance();

        playerDistance = Mathf.Abs(playerRef.transform.position.x - transform.position.x);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //play;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerTargeted = false;
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
            //playerRef.GetComponent<Player>().damage(2);
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
}
