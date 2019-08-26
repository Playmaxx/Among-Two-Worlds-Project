using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    int arrowDamage = 20;
    public int arrowspeed = 1;
    Rigidbody2D rigidRef;

    void Awake()
    {
        rigidRef = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidRef.gravityScale = 0;
        rigidRef.velocity = (Player.PlayerInstance.transform.position - transform.position).normalized * arrowspeed;
        transform.Rotate((Player.PlayerInstance.transform.position - transform.parent.transform.position).normalized);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            rigidRef.velocity = Vector2.zero;
        }
        if (collision.tag == "Player")
        {
            Player.PlayerInstance.damage(arrowDamage);
        }
    }
}
