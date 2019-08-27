using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGreatCollectable : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.score = GameManager.score +10;
            Destroy(this.gameObject);
        }
    }

}
