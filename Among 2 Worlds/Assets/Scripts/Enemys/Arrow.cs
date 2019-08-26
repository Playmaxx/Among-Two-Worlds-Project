using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    int arrowDamage = 20;
    public int arrowspeed = 1;
    Rigidbody2D rigidRef;
    bool isreflected = false;

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
        if (collision.tag == "Player" && rigidRef.velocity != Vector2.zero)
        {
            if (Player.PlayerInstance.shieldActive == false)
            {
                Player.PlayerInstance.damage(arrowDamage);
                Destroy(this.gameObject);
            }
            else
            {
                rigidRef.velocity = rigidRef.velocity * -1;
                Player.PlayerInstance.shieldActive = false;
                Player.PlayerInstance.currentShieldTime = 0;
                isreflected = true;
            }
        }
        if (collision.tag == "Enemy" && isreflected == true)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    IEnumerator despawnArrow()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
