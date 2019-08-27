using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public enum enemyState { Idle, Attacking, Death }
    public enemyState ArcherState;

    public int health = 100;
    float trackingdistance;
    bool playerInRange = false;
    bool playerTracked = false;
    Player playerRef;
    [SerializeField]
    GameObject ArrowPrefab;
    [SerializeField]
    int arrowCooldown = 3;
    float currentCooldown = 0;

    void Awake()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        trackingdistance = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange == false)
        {
            Debug.DrawLine(transform.position, playerRef.transform.position, Color.green);
        }
        if (playerInRange == true && playerTracked == false)
        {
            Debug.DrawLine(transform.position, playerRef.transform.position, new Color(1.0f, 0.64f, 0.0f));
        }
        if (playerInRange == true && playerTracked == true)
        {
            Debug.DrawLine(transform.position, playerRef.transform.position, Color.red);
        }
    }

    void FixedUpdate()
    {
        if (ArcherState != enemyState.Death)
        {
            if (playerInRange == true)
            {
                if (Physics2D.Raycast(transform.position, playerRef.transform.position.normalized, trackingdistance, GameManager.GMInstance.platformMask))
                {
                    playerTracked = false;
                }
                if (!Physics2D.Raycast(transform.position, playerRef.transform.position.normalized, trackingdistance, GameManager.GMInstance.platformMask))
                {
                    playerTracked = true;
                }
            }
            if (playerTracked == true)
            {
                StartCoroutine(shoot());
            }
            else
            {
                ArcherState = enemyState.Idle;
            }
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
            if (health <= 0)
            {
                StartCoroutine(death());
            }
            if (playerRef.transform.position.x > transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
            playerTracked = false;
        }
    }

    IEnumerator shoot()
    {
        if (currentCooldown <= 0)
        {
            ArcherState = enemyState.Attacking;
            currentCooldown = arrowCooldown;
            yield return new WaitForSeconds(0.4f);
            GameObject Arrow = Instantiate(ArrowPrefab, transform) as GameObject;
            Arrow.transform.position = transform.position;
        }
    }
    public IEnumerator death()
    {
        health = 0;
        ArcherState = enemyState.Death;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.05f);
        Destroy(this.gameObject);
    }
}
