using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyLoopSFX : MonoBehaviour
{
    private static DontDestroyLoopSFX instance = null;
    public static DontDestroyLoopSFX instance2
    {
        get { return instance; }
    }

    void Awake()
    {
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