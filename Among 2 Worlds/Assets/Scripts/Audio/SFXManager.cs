using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    Player playerRef;
    [SerializeField]
    Rigidbody2D rigidRef;
    [SerializeField]
    Knight knightRef;

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

    public void playSound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void CallJumpSound()
    {
        if (GameManager.GMInstance.currentdim == GameManager.dimension.Dark)
        {
            if (Input.GetKeyDown(KeyCode.Space) && JumpPlayed == false)
            {
                playSound(current_audioclip2, Jump);
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
                playSound(current_audioclip2, Jump);
                JumpPlayed = true;
            }
        }
        
        if (playerRef.playerMoveState == Player.moveState.Grounded)
        {
            JumpPlayed = false;
            JumpCounter = 1;
        }
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
                    playSound(current_audioclip, Dash);
                    DashPlayed = true;
                }
            }
        }

        if (playerRef.playerMoveState != Player.moveState.Dashing)
        {
            DashPlayed = false;
        }
    }

    public void CallLandSound()
    {
        if (playerRef.playerMoveState == Player.moveState.Grounded && LandPlayed == false)
        {
            playSound(current_audioclip, Land);
            LandPlayed = true;
            Debug.Log("hayyy");
        }
        
        if (playerRef.playerMoveState != Player.moveState.Grounded)
        {
            LandPlayed = false;
        }
    }
}
