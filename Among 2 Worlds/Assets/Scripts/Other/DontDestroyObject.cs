using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroyObject : MonoBehaviour
{
    public Text scoreText;
    public Canvas rootCanvas;
    private static DontDestroyObject instance = null;
    public static DontDestroyObject instance2
    {
        get { return instance; }
    }

    void Awake()
    {
        scoreText = GetComponent<Text>();
        rootCanvas = GetComponent<Canvas>();

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
}
