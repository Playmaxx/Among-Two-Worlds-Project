using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour     //manages aspects of the player that apply to both Gale and Lilian
{
    public enum direction { Left, Right }       // variables
    public direction playerdirection;
    public enum moveState { Grounded, Jumping, Falling, Dashing, Gliding, Walled, Other }
    public moveState playerMoveState = moveState.Grounded;

    float playerheight = 3.943503f;
    float playerwidth = 0.7109921f;
    public bool isGrounded;
    public bool isWalled;
    public int Jumps = 2;
    public float speedMultiplier = 0.13f;
    public int jumpforce = 7;
    public int dashspeed = 300;
    public bool dashused = false;
    public int glidespeed = 2;
    public int wallSlideSpeed = 2;

    public Rigidbody2D rigidRef;        //ref types
    public Gale galeRef;
    public Lilian lilianRef;
    public SpriteRenderer renderRef;
    public Vector2 moveRef;


    public int dashTimer = 10;


    private void Awake()
    {
        rigidRef = GetComponent<Rigidbody2D>();
        renderRef = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerdirection = direction.Right;
    }

    // Update is called once per frame
    void Update()
    {
        //general functions
        checkGroundState();
        refreshVariables();

        //handles character specific functions 
        switch (GameManager.GMInstance.currentdim)
        {
            case (GameManager.dimension.Light):
                lilianRef.movement();
                //lilianRef.jump();
                //lilianRef.dash();
                //lilianRef.glide();
                //lilianRef.wallaction();
                break;
            case (GameManager.dimension.Dark):
                galeRef.movement();
                //galeRef.jump();
                //galeRef.dash();
                break;

        }

        //tests

    }

    void refreshVariables()     //For variables that need to update every frame
    {
        float x = Input.GetAxis("Horizontal");
        float y = 0;
        moveRef = new Vector2(x, y);
        if (moveRef.x > 0) { playerdirection = direction.Right; }
        if (moveRef.x < 0) { playerdirection = direction.Left; }
    }

    void checkGroundState()     //checks if the player is grounded
    {
        RaycastHit2D[] downResult = new RaycastHit2D[1];
        Collider2D colliderRef = GetComponent<CapsuleCollider2D>();
        int hit = (colliderRef.Cast(Vector2.down, downResult, playerheight/2, true));
        if (hit != 0)
        {
            Debug.Log("test");
        }
    }

    void refreshAbilities()     //refreshes jumps & dashes etc.
    {
        Jumps = 2;
        dashused = false;
    }

}
