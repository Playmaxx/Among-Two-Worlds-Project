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
    public AudioClip Land;

    [SerializeField]
    bool JumpPlayed = false;
    [SerializeField]
    bool DashPlayed = false;
    [SerializeField]
    bool LandPlayed = false;
    [SerializeField]
    int JumpCounter = 1;

    //Awake is called before start
    void Awake()
    {
        playerRef = FindObjectOfType<Player>();
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
        CallJumpSound();
        CallDashSound();
    }

    public void playJumpSound()
    {
        current_audioclip2.clip = Jump;
        current_audioclip2.Play();
        Debug.Log("Jump-Sound-test");
    }

    public void CallJumpSound()
    {
        if (GameManager.GMInstance.currentdim == GameManager.dimension.Dark)
        {
            if (Input.GetKeyDown(KeyCode.Space) && JumpPlayed == false)
            {
                playJumpSound();
                JumpCounter--;
                Debug.Log(JumpPlayed);
            }
            
            if (JumpCounter < 0)
            {
                JumpCounter = 0;
                JumpPlayed = true;
            }
        }

        if (GameManager.GMInstance.currentdim == GameManager.dimension.Light)
        {
            if (playerRef.rigidRef.velocity.y > 0 && JumpPlayed == false)
            {
                playJumpSound();
                JumpPlayed = true;
                Debug.Log(JumpPlayed);
            }
        }
        
        if (playerRef.playerMoveState == Player.moveState.Grounded)
        {
            JumpPlayed = false;
            JumpCounter = 1;
        }
    }


    public void playDashSound()
    {
            current_audioclip.clip = Dash;
            current_audioclip.Play();
            Debug.Log("dash sound test");
    }
    
    public void CallDashSound()
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

        if (playerRef.playerMoveState != Player.moveState.Dashing)
        {
            DashPlayed = false;
        }
    }

    public void playLandSound()
    {
        current_audioclip.clip = Land;
        current_audioclip.Play();
        Debug.Log("played");
    }

    public void CallLandSound()
    {
        if (playerRef.playerMoveState == Player.moveState.Grounded && LandPlayed == false)
        {
            playLandSound();
            LandPlayed = true;
        }
        
        if (playerRef.playerMoveState != Player.moveState.Grounded)
        {
            LandPlayed = false;
        }
    }
    public void playKnightGettingHitSound()
    {
        current_audioclip.clip = KnightGettingHit;
        current_audioclip.Play();
        Debug.Log("KnightGitHit");
    }
}
