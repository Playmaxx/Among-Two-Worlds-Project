using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    Player playerRef;

    public AudioSource current_audioclip;
    public AudioClip Jump;
    public AudioClip Dash;

    //Awake is called before start
    void Awake()
    {
        playerRef = FindObjectOfType<Player>();
        current_audioclip = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        current_audioclip.volume = 1.0f;
        current_audioclip.clip = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playJumpSound()
    {

        switch (GameManager.GMInstance.currentdim)
        {
            case (GameManager.dimension.Light):
                if (playerRef.Jumps > 0)
                {
                    current_audioclip.clip = Jump;
                    current_audioclip.Play();
                    Debug.Log("Jump-Sound-test");
                }
                break;

            case (GameManager.dimension.Dark):
                if (playerRef.Jumps != 0)
                {
                    current_audioclip.clip = Jump;
                    current_audioclip.Play();
                    Debug.Log("Jump-Sound-test");
                }
                break;
        }
    }


    public void playDashSound()
    {
        if ((GameManager.GMInstance.currentdim == GameManager.dimension.Dark))
        {
            current_audioclip.clip = Dash;
            current_audioclip.Play();
            Debug.Log("dash sound test");
        }
    }
}
