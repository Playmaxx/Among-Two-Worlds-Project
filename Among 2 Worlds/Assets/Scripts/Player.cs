using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour     //manages aspects of the player that apply to both Gale and Lilian
{
    public enum direction { Left, Right }       // variables
    public direction playerdirection;

    public enum wallDirection { Left, Right }
    public wallDirection lastWallDirection;

    public enum moveState { Grounded, Jumping, Falling, Dashing, Gliding, Walled, Walljumping, Other }
    public moveState playerMoveState;

    [HideInInspector]
    public float playerheight;
    [HideInInspector]
    public float playerwidth;
    public int Jumps = 2;
    public int health = 100;
    public float moveSpeed = 7.5f;
    public int jumpforce = 20;
    public int dashspeed = 300;
    public bool dashused = false;
    public int glidespeed = 2;
    public float wallSlideSpeed = 0.1f;
    public float playerGravity = 10;
    public float maxGlideTime = 5;
    public float currentGlideTime = 0;
    public float dashtime = 0.5f;
    public float currentDashTime = 0;
    public float wallJumpCD = 0.5f;
    public float currentWallJump = 0;
    public bool DeathSequenceIsPlaying = false;

    //ref types
    Gale galeRef;
    Lilian lilianRef;
    public Rigidbody2D rigidRef;
    public SpriteRenderer renderRef;
    Vector2 downVector;


    private void Awake() //is called before start, catch references here
    {
        rigidRef = GetComponent<Rigidbody2D>();
        renderRef = GetComponent<SpriteRenderer>();
        galeRef = GetComponent<Gale>();
        lilianRef = GetComponent<Lilian>();
        playerheight = GetComponent<CapsuleCollider2D>().size.y;
        playerwidth = GetComponent<CapsuleCollider2D>().size.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerdirection = direction.Right;
        rigidRef.gravityScale = 20;
        playerMoveState = moveState.Falling;
        downVector = new Vector2(Vector2.down.x, Vector2.down.y + 0.15f) * playerheight / 2;
        Debug.Log(downVector);
        Debug.Log(downVector.y);
        downVector = Vector2.down * playerheight / 2;
        Debug.Log(downVector);
        Debug.Log(downVector.y);
        downVector = new Vector2(Vector2.down.x, Vector2.down.y) * playerheight / 2;
        downVector = new Vector2(downVector.x, downVector.y + 0.15f);
        Debug.Log(downVector);
        Debug.Log(downVector.y);
    }

    // Update is called once per frame
    void Update()
    {
        //general functions
        groundCheck();
        refreshVariables();
        matchMoveState();

        //handles character specific functions
        switch (GameManager.GMInstance.currentdim)
        {
            case (GameManager.dimension.Light):
                lilianRef.movement();
                lilianRef.jump();
                lilianRef.dash();
                lilianRef.glide();
                lilianRef.wallaction();
                break;
            case (GameManager.dimension.Dark):
                galeRef.movement();
                galeRef.jump();
                galeRef.dash();
                galeRef.wallaction();
                break;

        }

        DeathSequence();

        //tests
        Debug.DrawRay(new Vector2(transform.position.x + (playerwidth / 2) - 0.05f, transform.position.y), downVector, Color.green);
        Debug.DrawRay(new Vector2(transform.position.x - (playerwidth / 2) + 0.05f, transform.position.y), downVector, Color.green);
        Debug.DrawRay(transform.position, Vector2.left * playerwidth / 2, Color.green);
        Debug.DrawRay(transform.position, Vector2.right * playerwidth / 2, Color.green);
        //utilized raycast overload: origin, direction, distance, layermask
        //origin is transform.position +/- playerwidth/2 for x
        // direction is downwards
        //distance is playerheight/2
        //layermask is gamemanager.platformmask
    }

    void refreshVariables()     //For variables that need to update every frame
    {
        if (playerdirection == direction.Right)
        {
            renderRef.flipX = true;
        }
        if (playerdirection == direction.Left)
        {
            renderRef.flipX = false;
        }
        currentDashTime -= 1 * Time.deltaTime;
    }

    public void refreshAbilities()     //refreshes jumps & dashes etc.
    {
        Jumps = 2;
        dashused = false;
        currentGlideTime = 0;
    }

    //checks ground state
    void groundCheck()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x + (playerwidth / 2) - 0.15f, transform.position.y), Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask))
        {
            if (playerMoveState != moveState.Dashing)
            {
                playerMoveState = moveState.Grounded;
                refreshAbilities();
                Debug.Log("rightray");
            }
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x - (playerwidth / 2) + 0.05f, transform.position.y), Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask))
        {
            if (playerMoveState != moveState.Dashing)
            {
                playerMoveState = moveState.Grounded;
                refreshAbilities();
                Debug.Log("leftray");
            }
        }
        if (!Physics2D.Raycast(new Vector2(transform.position.x + (playerwidth / 2) - 0.05f, transform.position.y), Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask))
        {
            if (!Physics2D.Raycast(new Vector2(transform.position.x - (playerwidth / 2) + 0.05f, transform.position.y), Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask))
            {
                if (playerMoveState != moveState.Dashing)
                {
                    if (rigidRef.velocity.y > 0)
                    {
                        playerMoveState = moveState.Jumping;
                        Debug.Log("jumping");
                    }
                    else if (playerMoveState != moveState.Gliding)
                    {
                        playerMoveState = moveState.Falling;
                        Debug.Log("falling");
                    }
                }
            }
        }
    }

    void matchMoveState()
    {
        if (playerMoveState == moveState.Gliding)
        {
            if (Physics2D.Raycast(new Vector2(transform.position.x - (playerwidth / 2) + 0.05f, transform.position.y), Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask))
            {
                playerMoveState = moveState.Falling;
            }
            if (Physics2D.Raycast(new Vector2(transform.position.x + (playerwidth / 2) - 0.05f, transform.position.y), Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask))
            {
                playerMoveState = moveState.Falling;
            }
        }

        if (playerMoveState == moveState.Dashing && Physics2D.Raycast(transform.position, Vector2.left, playerwidth / 2, GameManager.GMInstance.platformMask))
        {
            lastWallDirection = wallDirection.Left;
            playerMoveState = moveState.Walled;
            currentDashTime = -1;
            rigidRef.velocity = Vector2.zero;
        }

        if (playerMoveState == moveState.Dashing && Physics2D.Raycast(transform.position, Vector2.right, playerwidth / 2, GameManager.GMInstance.platformMask))
        {
            lastWallDirection = wallDirection.Right;
            playerMoveState = moveState.Walled;
            currentDashTime = -1;
            rigidRef.velocity = Vector2.zero;
        }

        switch (playerMoveState)    //movestates: Grounded, Jumping, Falling, Dashing, Gliding, Walled, Other
        {
            case (moveState.Grounded):
                rigidRef.gravityScale = 0;
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, 0);
                break;

            case (moveState.Jumping):
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, rigidRef.velocity.y - 1);
                break;

            case (moveState.Falling):
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, rigidRef.velocity.y - 1);
                break;

            case (moveState.Dashing):
                break;

            case (moveState.Gliding):
                if (currentGlideTime < maxGlideTime)
                {
                    rigidRef.velocity = new Vector2(rigidRef.velocity.x, -glidespeed);
                }
                currentGlideTime += 1 * Time.deltaTime;
                break;

            case (moveState.Walled):
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, -glidespeed);
                break;

            case (moveState.Walljumping):
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, rigidRef.velocity.y - 1);
                break;

            case (moveState.Other):
                break;
        }
    }

    public void damage(int amount)
    {
        health -= amount;
    }

    public void heal(int amount)
    {
        health += amount;
    }

    void DeathSequence()
    {
        if (Input.GetKey(KeyCode.F) && DeathSequenceIsPlaying == false)
        {
            DeathSequenceIsPlaying = true;
            StartCoroutine(RespawnPlayerAfterTime(3));
        }
    }

    IEnumerator RespawnPlayerAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        transform.position = new Vector2(0.299f, 2f);
        DeathSequenceIsPlaying = false;
        rigidRef.velocity = new Vector2(0, 0);
    }
}
