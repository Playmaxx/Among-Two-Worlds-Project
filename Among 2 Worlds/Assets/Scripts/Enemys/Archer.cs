using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    int health = 100;
    public int arrowspeed = 1;
    float trackingdistance;
    bool playerInRange = false;
    bool playerTracked = false;
    Player playerRef;

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
            shoot();
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

    void shoot()
    {
        transform.GetChild(0).GetComponent<Transform>().position = transform.position;
        transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = (playerRef.transform.position - transform.position).normalized * arrowspeed;
    }
}
