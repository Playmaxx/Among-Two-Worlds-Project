using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject panel;
    public PopUps PopUpInstance;

    private void Awake()
    {
        if (PopUpInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            PopUpInstance = this;
        }
        else if (PopUpInstance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            panel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            panel.SetActive(false);
        }
    }

    public void updatePanels()
    {
        panel = GameObject.FindGameObjectWithTag("Textbox");
    }
}
