using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSFXManager : MonoBehaviour
{
    [SerializeField]
    AudioSource current_audioclip;
    public AudioClip Step;
    bool StepPlayed = false;

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
    }

    // Update is called once per frame
    void Update() //Inhalt ist nur für mich zum testen und kann gelöscht werden
    {
        if (playerRef.playerMoveState == Player.moveState.Grounded && rigRef.velocity != new Vector2(0, 0))
        {
            if (StepPlayed == false)
            {
                playStepSound();
                StepPlayed = true;
                Debug.Log("playin");
            }
        }

        if (rigRef.velocity == new Vector2(0, 0) || playerRef.playerMoveState != Player.moveState.Grounded)
        {
            stopStepSound();
            StepPlayed = false;
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
}
