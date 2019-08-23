using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSFXManager : MonoBehaviour
{
    [SerializeField]
    AudioSource playerStepSource;
    [SerializeField]
    AudioSource playerRattleSource;
    [SerializeField]
    AudioSource KnightStepSource;

    public AudioClip rattle;
    public AudioClip Step;
    public AudioClip KnightStep;
    bool playerSoundPlayed = false;
    bool knightSoundPlayed = false;

    [SerializeField]
    Player playerRef;
    [SerializeField]
    Rigidbody2D playerRigRef;
    [SerializeField]
    Knight knightRef;
    [SerializeField]
    Rigidbody2D knightRigRef;

    void Awake()
    {
        knightRef = GetComponent<Knight>();
        //knightRigRef = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerStepSource.volume = 1.0f;
        playerStepSource.clip = null;
        playerRattleSource.volume = 1.0f;
        playerRattleSource.clip = null;
    }

    // Update is called once per frame
    void Update() //Inhalt ist nur für mich zum testen und kann gelöscht werden
    {
        ManagePlayerRunSound();
    }

    void ManagePlayerRunSound()
    {
        if (playerRef.playerMoveState == Player.moveState.Grounded && playerRigRef.velocity != new Vector2(0, 0))
        {
            if (playerSoundPlayed == false)
            {
                playSound(playerStepSource, Step);
                playSound(playerRattleSource, rattle);
                playerSoundPlayed = true;
            }
        }

        if (playerRigRef.velocity == new Vector2(0, 0) || playerRef.playerMoveState != Player.moveState.Grounded)
        {
            stopSound(playerStepSource, Step);
            stopSound(playerRattleSource, rattle);
            playerSoundPlayed = false;
        }
    }

    public void ManageKnightRunSound()
    {
        if (knightRef.knightState == Knight.enemyState.Patrolling || knightRef.knightState == Knight.enemyState.Following)
        {
            playSound(KnightStepSource, KnightStep);
            knightSoundPlayed = true;
        }

        if (knightRef.knightState != Knight.enemyState.Patrolling && knightRef.knightState != Knight.enemyState.Following)
        {
            stopSound(KnightStepSource, KnightStep);
            knightSoundPlayed = false;
        }
    }
        
    void playSound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void stopSound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Stop();
    }
}
