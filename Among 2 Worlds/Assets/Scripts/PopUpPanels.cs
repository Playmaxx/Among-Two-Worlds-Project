using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPanels : MonoBehaviour
{
    public GameObject panel_jump;
    public CanvasGroup canvasGroup;
    bool state;
    // Start is called before the first frame update
    void Awake()
    {
        panel_jump = GetComponent<GameObject>();
    }

    public void SwitchHideShow()
    {
        state = !state;
        panel_jump.gameObject.SetActive(state);
    }

    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            SwitchHideShow();
        }
    }
}
