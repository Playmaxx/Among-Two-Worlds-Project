using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject[] panels = new GameObject[2];
    //public GameObject panelRef;
    Player playerRef;
    //bool state = false;

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
        //panelRef.SetActive(state);
        if (playerRef.transform.position.x < -1.5)
        {
            Debug.Log("player is on dis position");
            panels[0].SetActive(true);
            //state = !state;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //panels[0].SetActive(false);
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
            //panelRef = gameObject.transform.Find("Panel(Worldshifting)").gameObject;
            //state = !state;
        //}
    }
}
