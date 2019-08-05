using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject[] panels = new GameObject[2];
    //public GameObject panelRef;
    Player playerRef;

    void Start()
    {
        panels[0] = gameObject.transform.Find("Panel(Jump)").gameObject;
        panels[1] = gameObject.transform.Find("Panel(Worldshifting)").gameObject;

        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
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
        if(collision.tag == "TriggerBox-Jump")
        {
            panels[0].SetActive(true);
            Debug.Log("Display Jump-PopUp");
        }

        if (collision.tag == "TriggerBox-Worldshift")
        {
            panels[1].SetActive(true);
            panels[0].SetActive(false);
            Debug.Log("Display Worldshift-PopUp");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TriggerBox-Jump")
        {
            panels[0].SetActive(false);
            //transform.gameObject.tag = "TriggerBox-Worldshift";
            Debug.Log("Hide Jump-PopUp");
        }

        if (collision.tag == "TriggerBox-Worldshift")
        {
            panels[1].SetActive(false);
            Debug.Log("Hide Jump-PopUp");
        }
    }
}
