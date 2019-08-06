using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject panel;
    //public GameObject panelRef;
    Player playerRef;

    void Start()
    {
        panel = gameObject.transform.Find("Pop-Up-Panel").gameObject;
        panel.SetActive(false);
    }
    
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        //{
            //panels[0].SetActive(false);
            //transform.gameObject.tag = "TriggerBox-Worldshift";
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.tag == "Player")
            {
                panel.SetActive(true);
                Debug.Log("Display panel");
            }
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        {
            if (collision.tag == "Player")
            {
                panel.SetActive(false);
                Debug.Log("Hide panel");
            }
            
        }
    }
}
