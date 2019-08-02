using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    GameObject[] panels;
    public GameObject panelRef;
    bool state = false;

    void Awake()
    {
        panels = new GameObject[2];
        panels[0] = gameObject.transform.Find("Panel(Jump)").gameObject;
        panels[1] = gameObject.transform.Find("Panel(Worldshifting)").gameObject;

        foreach(GameObject panel in panels)
        {
            panel.SetActive(state);
        }
    }
    
    void Update()
    {
        //panelRef.SetActive(state);
        if (Input.GetKeyDown(KeyCode.P))
        {
            panelRef = gameObject.transform.Find("Panel(Jump)").gameObject;
            state = !state;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            panelRef = gameObject.transform.Find("Panel(Worldshifting)").gameObject;
            state = !state;
        }
    }
}
