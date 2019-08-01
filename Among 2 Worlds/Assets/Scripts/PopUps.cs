using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject panelRef;
    bool state = false;

    void Awake()
    {
        panelRef = gameObject.transform.Find("Panel(Jump)").gameObject;
    }
    
    void Update()
    {
        panelRef.SetActive(state);
        if (Input.GetKeyDown(KeyCode.P))
        {
            state = !state;
        }

        //if (Input.GetKeyDown(KeyCode.O))
        //{
            //panelRef = gameObject.transform.Find("Panel(Worldshifting)").gameObject;
            //state = !state;
        //}
    }
}
