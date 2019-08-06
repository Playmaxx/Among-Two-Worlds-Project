using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
   
    Player playerRef;
    public Animator animator;

    //Awake is called before Start.
    private void Awake()
    {
       
        playerRef = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }

    void CheckState() //Checks the different Variables necessery for Animations
    {
        bool LightDimension;   //Checks which Dimension is active right now
        if (GameManager.GMInstance.currentdim == GameManager.dimension.Light)
        {
            LightDimension = true;
        }
        else
        {
            LightDimension = false;
        }
        animator.SetBool("LightDimension", LightDimension);


        bool isGrounded;  //Checks if player is Grounded
        if (playerRef.playerMoveState == Player.moveState.Grounded)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        animator.SetBool("isGrounded", isGrounded);

        bool isJumping;   //Checks if player is Jumping
        if (playerRef.playerMoveState == Player.moveState.Jumping)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
        animator.SetBool("isJumping", isJumping);

        bool isGliding;   //Checks if player is Gliding
        if (playerRef.playerMoveState == Player.moveState.Gliding)
        {
            isGliding = true;
        }
        else
        {
            isGliding = false;
        }
        animator.SetBool("isGliding", isGliding);


        bool isFalling;    //Checks if player is falling down
        if (playerRef.playerMoveState == Player.moveState.Falling)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
        animator.SetBool("isFalling", isFalling);

        bool isWalled;
        if(playerRef.playerMoveState == Player.moveState.Walled)
        {
            isWalled = true;
        }
        else
        {
            isWalled = false;
        }
        animator.SetBool("isWalled", isWalled);


        int NumberOfJumps = playerRef.Jumps;   //Check needed for doublejumping
        animator.SetInteger("NumberOfJumps", NumberOfJumps);

        //Checks if the character is moving
        float characterSpeed = Mathf.Abs(playerRef.rigidRef.velocity.x);
        animator.SetFloat("Speed", characterSpeed);
    }
}
