using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroyHealthbar : MonoBehaviour
{
    public Slider HealthSlider2;
    public Canvas rootCanvas;
    private static DontDestroyHealthbar le_instance = null;
    public static DontDestroyHealthbar le_instance2
    {
        get { return le_instance; }
    }

    void Awake()
    {
        rootCanvas = GetComponent<Canvas>();
        HealthSlider2 = GameObject.Find("Slider").GetComponent<Slider>();

        if (le_instance != null && le_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {
            le_instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
