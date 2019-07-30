using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public AudioSource p_audio;
    public AudioClip Jump;
    public AudioClip Dash;
    Player playerRef;


    // Start is called before the first frame update
    void Start()
    {
        p_audio = GetComponent<AudioSource>();
        p_audio.volume = 1.0f;
        p_audio.clip = null;
        playerRef = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            p_audio.clip = Jump;
            p_audio.Play();
        }

        if (GameManager.GMInstance.currentdim == GameManager.dimension.Dark)
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
            {
                if (playerRef.dashused == false)        //press shift + a||d to trigger
                {
                    p_audio.clip = Dash;
                    p_audio.Play();
                }

            }
        }
        
    }
}
