using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : MonoBehaviour
{
    int leftPatrolX;
    int rightPatrolX;
    int attackRange;
    bool playerTargeted = false;

    Player playerRef;

    void Awake()
    {
        playerRef = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        closeDistance();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerTargeted = true;
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
        if(playerTargeted == true)
        {
            //if(playerRef.transform.x <)
        }
    }
}
