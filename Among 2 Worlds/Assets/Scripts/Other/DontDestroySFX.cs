using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySFX : MonoBehaviour
{
    private static DontDestroySFX instance = null;
    public static DontDestroySFX instance2
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