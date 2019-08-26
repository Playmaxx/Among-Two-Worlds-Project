using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLever : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GMInstance.TowerLever == true)
        {
            Destroy(this);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.GMInstance.TowerLever = true;
            Debug.Log("Got'em");
            Destroy(this);
        }
    }
}
