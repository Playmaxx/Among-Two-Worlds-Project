using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public AudioSource p_audio;
    public AudioClip Jump;
    public AudioClip Dash;

    // Start is called before the first frame update
    void Start()
    {
        p_audio = GetComponent<AudioSource>();
        p_audio.volume = 0.5f;
        p_audio.clip = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            p_audio.clip = Jump;
            p_audio.Play();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.A)))
        {
            p_audio.clip = Dash;
            p_audio.Play();
        }
    }
}
