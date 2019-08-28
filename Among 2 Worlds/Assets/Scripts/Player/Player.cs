using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour     //manages aspects of the player that apply to both Gale and Lilian, manages gale/lilian functions
{
    //enums
    public enum direction { Left, Right }
    public direction playerdirection;

    public enum wallDirection { Left, Right }
    public wallDirection lastWallDirection;

    public enum moveState { Grounded, Jumping, Falling, Dashing, Gliding, Walled, Walljumping, Other }
    public moveState playerMoveState;

    //tweakable variables
    public int Jumps = 2;
    public static int health = 100;
    public float moveSpeed = 7.5f;
    public int jumpforce = 20;
    public int dashspeed = 300;
    public int WJSpeed = 15;
    public int glidespeed = 2;
    public float wallSlideSpeed = 0.1f;
    public float wallJumpCcoolDown = 0.5f;
    public float currentWallJump = 0;
    public float wallJumpXSpeed = 5;
    public float terminalVelocity = 10;

    //timers
    public float dashtime = 0.5f;
    public float currentDashTime = 0;
    public float dashCooldown = -2;
    public float maxGlideTime = 5;
    public float currentGlideTime = 0;
    public float shieldTime = 5;
    public float currentShieldTime = -5;
    public float shieldCooldown = -5;
    public float wallJumpTime = 0.5f;
    public float currentWJTime = 0;
    public float iFrameTime = 2;
    public float currentITime = 0;
    public float frameswitchtime = 0.2f;

    //non-tweakable variables
    public static Player PlayerInstance;
    [HideInInspector]
    public float playerheight;
    [HideInInspector]
    public float playerwidth;
    public bool shieldActive = false;
    public bool DeathSequenceIsPlaying = false;
    public bool dashused = false;
    public bool WJUsed = false;
    public bool glideUsed = false;
    float frameSwitchMultiplier = 0;

    //ref types
    Gale galeRef;
    Lilian lilianRef;
    public Rigidbody2D rigidRef;
    public SpriteRenderer renderRef;
    Vector2 downVector;


    private void Awake() //is called before start, catch references here
    {
        if (PlayerInstance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            PlayerInstance = this;
        }
        else if (PlayerInstance != this)
        {
            Destroy(this.gameObject);
        }
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
        rigidRef.gravityScale = 0;
        playerMoveState = moveState.Falling;
        /*
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
        */
    }

    //update is called once per frame, graphical stuff here
    void Update()
    {
        Debug.DrawRay(new Vector2(transform.position.x + (playerwidth / 2) - 0.05f, transform.position.y), downVector, Color.green);
        Debug.DrawRay(new Vector2(transform.position.x - (playerwidth / 2) + 0.05f, transform.position.y), downVector, Color.green);
        Debug.DrawRay(transform.position, Vector2.left * playerwidth, Color.red);
        Debug.DrawRay(transform.position, Vector2.right * playerwidth, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * playerwidth / 2, Color.green);
        Debug.DrawRay(transform.position, Vector2.right * playerwidth / 2, Color.green);
    }

    // FixedUpdate is called 60 times/s, physics/logic stuff here
    void FixedUpdate()
    {
        if (playerMoveState != moveState.Other)
        {
            //general functions
            groundCheck();
            refreshVariables();
            matchMoveState();

            if (health > 0)
            {
                //handles character specific functions
                switch (GameManager.GMInstance.currentdim)
                {
                    case (GameManager.dimension.Light):
                        lilianRef.movement();
                        lilianRef.jump();
                        lilianRef.shield();
                        lilianRef.glide();
                        lilianRef.wallaction();
                        break;
                    case (GameManager.dimension.Dark):
                        galeRef.movement();
                        galeRef.jump();
                        galeRef.dash();
                        galeRef.cancelGlide();
                        galeRef.wallaction();
                        break;

                }
            }
        }
    }

    void refreshVariables()     //For variables that need to update every frame, e.g. timers
    {
        if (playerdirection == direction.Right)
        {
            renderRef.flipX = true;
        }
        if (playerdirection == direction.Left)
        {
            renderRef.flipX = false;
        }
        if (currentShieldTime >= shieldCooldown)
        {
            currentShieldTime -= 1 * Time.deltaTime;
        }
        if (GameManager.GMInstance.currentdim == GameManager.dimension.Dark)
        {
            shieldActive = false;
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            if (currentShieldTime > 0)
            {
                currentShieldTime = 0;
            }
        }
        if (currentGlideTime >= 0)
        {
            currentGlideTime -= 1 * Time.deltaTime;
        }
        if (currentDashTime >= dashCooldown)
        {
            currentDashTime -= 1 * Time.deltaTime;
        }
        if (health <= 0)
        {
            rigidRef.velocity = Vector2.zero;
            StartCoroutine(RespawnPlayerAfterTime(3));
        }
        if (currentITime > 0)
        {
            iFrameFlash();
            currentITime -= 1 * Time.deltaTime;
        }
        else
        {
            renderRef.enabled = true;
        }
    }

    public void refreshAbilities()     //refreshes jumps & dashes etc.
    {
        Jumps = 2;
        dashused = false;
        WJUsed = false;
        glideUsed = false;
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
            }
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x - (playerwidth / 2) + 0.05f, transform.position.y), Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask))
        {
            if (playerMoveState != moveState.Dashing)
            {
                playerMoveState = moveState.Grounded;
                refreshAbilities();
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
                        if (playerMoveState == moveState.Walled)
                        {
                            playerMoveState = moveState.Walljumping;
                        }
                        else if (playerMoveState != moveState.Walljumping)
                        {
                            playerMoveState = moveState.Jumping;
                        }
                    }
                    else if (playerMoveState != moveState.Gliding)
                    {
                        playerMoveState = moveState.Falling;
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
        if (playerMoveState != moveState.Dashing)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        if (playerMoveState == moveState.Dashing)
        {
            GetComponent<TrailRenderer>().enabled = true;
        }
        else
        {
            GetComponent<TrailRenderer>().enabled = false;
        }
        if (playerMoveState == moveState.Gliding)
        {
            GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();
        }
        if (GameManager.GMInstance.currentdim == GameManager.dimension.Light && playerMoveState == moveState.Dashing)
        {
            playerMoveState = moveState.Falling;
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
                if (rigidRef.velocity.y < terminalVelocity)
                {
                    rigidRef.velocity = new Vector2(rigidRef.velocity.x, rigidRef.velocity.y - 1);
                }
                break;

            case (moveState.Dashing):
                GetComponent<CircleCollider2D>().enabled = true;
                break;

            case (moveState.Gliding):
                if (currentGlideTime > 0)
                {
                    rigidRef.velocity = new Vector2(rigidRef.velocity.x, -glidespeed);
                }
                currentGlideTime -= 1 * Time.deltaTime;
                if (currentGlideTime <= 0)
                {
                    playerMoveState = moveState.Falling;
                    glideUsed = true;
                }
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
        if (shieldActive == false)
        {
            if (currentITime <= 0)
            {
                currentITime = iFrameTime;
                frameSwitchMultiplier = 0;
                GetComponent<SpriteRenderer>().color = Color.red;
                health -= amount;
                StartCoroutine(decolorize());
            }
        }
        else
        {
            shieldActive = false;
            currentShieldTime = 0;
        }
    }

    public void heal(int amount)
    {
        if (health > 0)
        {
            health += amount;
        }
    }

    IEnumerator RespawnPlayerAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        transform.position = new Vector2(0.299f, 2f);
        DeathSequenceIsPlaying = false;
        rigidRef.velocity = new Vector2(0, 0);
        health = 100;
        if (GameManager.GMInstance.OriginScene == "Level 5")
        {
            GameObject.Find("Witch").transform.position = new Vector2(0, 26);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision is CapsuleCollider2D)
            {
                collision.gameObject.GetComponent<Knight>().health = 0;
            }
            if (collision is BoxCollider2D)
            {
                collision.gameObject.GetComponent<Archer>().health = 0;
            }
        }
    }

    IEnumerator decolorize()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void iFrameFlash()
    {
        if (currentITime < iFrameTime - frameSwitchMultiplier)
        {
            switchSpriteState();
            frameSwitchMultiplier += frameswitchtime;
        }
    }

    void switchSpriteState()
    {
        if (renderRef.enabled == true)
        {
            renderRef.enabled = false;
        }
        else
        {
            renderRef.enabled = true;
        }
    }
}
