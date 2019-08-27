using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int HealthToGive = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.PlayerInstance.heal(HealthToGive);
            Destroy(this.gameObject);
        }
    }
}
