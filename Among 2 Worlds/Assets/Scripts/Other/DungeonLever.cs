using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLever : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GMInstance.DungeonLever == true)
        {
            //flip sprite
            Destroy(this);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.GMInstance.DungeonLever = true;
        }
    }

}
