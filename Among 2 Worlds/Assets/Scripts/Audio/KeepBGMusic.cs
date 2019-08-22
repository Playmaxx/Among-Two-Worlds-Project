using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBGMusic : MonoBehaviour
{

    private static KeepBGMusic audio_instance = null;
    public static KeepBGMusic audio_instance2
    {
        get { return audio_instance; }
    }

    void Awake()
    {
        if (audio_instance != null && audio_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {
            audio_instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
