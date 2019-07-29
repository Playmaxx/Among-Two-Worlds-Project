using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour     //manages aspects of the player that apply to both Gale and Lilian
{
    public enum direction { Left, Right }       // variables
    public direction playerdirection;
    float playerheight = 3.943503f;
    float playerwidth = 0.7109921f;
    public bool isGrounded;
    public bool isWalled;
    public int Jumps = 2;
    public int speedMultiplier = 10;
    public int jumpforce = 7;
    public float longJumpMultiplier = 2f;
    public float gravityMultiplier = 2.5f;
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
                lilianRef.movement(moveRef);
                lilianRef.jump();
                lilianRef.dash(moveRef);
                lilianRef.glide();
                lilianRef.wallaction();
                break;
            case (GameManager.dimension.Dark):
                galeRef.movement(moveRef);
                galeRef.jump();
                galeRef.dash(moveRef);
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

        if (Physics2D.Raycast(rigidRef.position, Vector2.down, playerheight / 2))
        {
            refreshAbilities();
            isGrounded = true;
        }
        else
        {
            if (isGrounded == true)
            {
                Jumps--;
            }
            isGrounded = false;
        }
        if (Physics2D.Raycast(rigidRef.position, Vector2.left, playerwidth / 2) || Physics2D.Raycast(rigidRef.position, Vector2.right, playerwidth / 2))
        {
            isWalled = true;
        }
        else
        {
            isWalled = false;
        }
    }

    void refreshAbilities()     //refreshes jumps & dashes etc.
    {
        Jumps = 2;
        dashused = false;
    }

}
