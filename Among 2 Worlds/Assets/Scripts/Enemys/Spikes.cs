using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int spikeDamage;
    public float spikeCooldown = 3;
    [SerializeField]
    float currentCD = 0;
    public bool inSpikes = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentCD > 0)
        {
            currentCD -= 1 * Time.deltaTime;
        }
        if (inSpikes == true && currentCD < 0)
        {
            Player.PlayerInstance.damage(spikeDamage);
            currentCD = spikeCooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.PlayerInstance.damage(spikeDamage);
            currentCD = spikeCooldown;
            inSpikes = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inSpikes = false;
        }
    }
}
