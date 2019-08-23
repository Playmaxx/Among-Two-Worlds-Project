using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSFXManager : MonoBehaviour
{
    [SerializeField]
    AudioSource current_audioclip;
    [SerializeField]
    AudioSource current_audioclip2;

    public AudioClip rattle;
    public AudioClip Step;
    bool soundPlayed = false;

    [SerializeField]
    Player playerRef;
    [SerializeField]
    Rigidbody2D rigRef;

    void Awake()
    {
        current_audioclip = GetComponent<AudioSource>();
    }

    void Start()
    {
        current_audioclip.volume = 1.0f;
        current_audioclip.clip = null;
        current_audioclip2.volume = 1.0f;
        current_audioclip2.clip = null;
    }

    // Update is called once per frame
    void Update() //Inhalt ist nur für mich zum testen und kann gelöscht werden
    {
        if (playerRef.playerMoveState == Player.moveState.Grounded && rigRef.velocity != new Vector2(0, 0))
        {
            if (soundPlayed == false)
            {
                playStepSound();
                playRattleSound();
                soundPlayed = true;
            }
        }

        if (rigRef.velocity == new Vector2(0, 0) || playerRef.playerMoveState != Player.moveState.Grounded)
        {
            stopStepSound();
            stopRattleSound();
            soundPlayed = false;
        }
    }

    public void playStepSound()
    {
        current_audioclip.clip = Step;
        current_audioclip.Play();
    }

    public void stopStepSound()
    {
        current_audioclip.clip = Step;
        current_audioclip.Stop();
    }

    public void playRattleSound()
    {
        current_audioclip2.clip = rattle;
        current_audioclip2.Play();
    }

    public void stopRattleSound()
    {
        current_audioclip2.clip = rattle;
        current_audioclip2.Stop();
    }
}
