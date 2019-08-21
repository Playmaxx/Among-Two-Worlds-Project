using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    Player playerRef;
    Rigidbody2D rigidRef;

    public AudioSource current_audioclip;
    public AudioSource current_audioclip2;
    public AudioClip Jump;
    public AudioClip Dash;
    public AudioClip KnightGettingHit;

    [SerializeField]
    bool JumpPlayed = false;
    [SerializeField]
    bool DashPlayed = false;

    //Awake is called before start
    void Awake()
    {
        playerRef = Player.PlayerInstance;
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
        Call_PlayJumpSound();
        Call_PlayDashSound();
    }

    public void playJumpSound()
    {
        current_audioclip2.clip = Jump;
        current_audioclip2.Play();
    }

    public void Call_PlayJumpSound()
    {
        if (GameManager.GMInstance.currentdim == GameManager.dimension.Dark)
        {
            if (Input.GetKeyDown(KeyCode.Space) && JumpPlayed == false)
            {
                playJumpSound();
                JumpPlayed = true;
                Debug.Log(JumpPlayed);
            }
        }

        if (GameManager.GMInstance.currentdim == GameManager.dimension.Light)
        {
            if (Player.PlayerInstance.rigidRef.velocity.y > 0 && JumpPlayed == false)
            {
                playJumpSound();
                JumpPlayed = true;
            }
        }
        
        if (Player.PlayerInstance.playerMoveState == Player.moveState.Grounded)
        {
            JumpPlayed = false;
        }
    }


    public void playDashSound()
    {
            current_audioclip.clip = Dash;
            current_audioclip.Play();
            Debug.Log("dash sound test");
    }
    
    public void Call_PlayDashSound()
    {
        if ((GameManager.GMInstance.currentdim == GameManager.dimension.Dark))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (DashPlayed == false)
                    playDashSound();
                    DashPlayed = true;
                }
            }
        }

        if (Player.PlayerInstance.playerMoveState != Player.moveState.Dashing)
        {
            DashPlayed = false;
        }
    }
    public void playKnightGettingHitSound()
    {
        current_audioclip.clip = KnightGettingHit;
        current_audioclip.Play();
        Debug.Log("KnightGitHit");
    }
}
