using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorRattle : MonoBehaviour
{
    [SerializeField]
    AudioSource current_audioclip;
    public AudioClip rattle;
    bool rattlePlayed = false;

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
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) || playerRef.playerMoveState == Player.moveState.Grounded && rigRef.velocity != new Vector2(0, 0))
        {
            if (rattlePlayed == false)
            {
                playRattleSound();
                rattlePlayed = true;
                Debug.Log("rattle sound played");
            }
        }

        if ((Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A)) || playerRef.playerMoveState != Player.moveState.Grounded)
        {
            stopRattleSound();
            rattlePlayed = false;
            Debug.Log("rattle sound stopped");
        }
    }

    public void playRattleSound()
    {
        current_audioclip.clip = rattle;
        current_audioclip.Play();
    }

    public void stopRattleSound()
    {
        current_audioclip.clip = rattle;
        current_audioclip.Stop();
    }
}
