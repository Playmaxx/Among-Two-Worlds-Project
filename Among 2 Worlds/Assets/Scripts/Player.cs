﻿using System.Collections;
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
    public float moveSpeed = 7.5f;
    public int jumpforce = 20;
    public int dashspeed = 300;
    public bool dashused = false;
    public int glidespeed = 2;
    public int wallSlideSpeed = 2;
    public float playerGravity = 10;

    public Rigidbody2D rigidRef;        //ref types
    public Gale galeRef;
    public Lilian lilianRef;
    public SpriteRenderer renderRef;
    public Vector2 moveRef;


    private void Awake()
    {
        rigidRef = GetComponent<Rigidbody2D>();
        renderRef = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerdirection = direction.Right;
        rigidRef.gravityScale = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //general functions
        refreshVariables();
        matchMoveState();

        //handles character specific functions 
        switch (GameManager.GMInstance.currentdim)
        {
            case (GameManager.dimension.Light):
                //lilianRef.movement();
                //lilianRef.jump();
                //lilianRef.dash();
                //lilianRef.glide();
                //lilianRef.wallaction();
                break;
            case (GameManager.dimension.Dark):
                galeRef.movement();
                galeRef.jump();
                //galeRef.dash();
                break;

        }

        //tests
        Debug.Log(playerMoveState);

    }

    void refreshVariables()     //For variables that need to update every frame
    {
        if (moveRef.x > 0) { playerdirection = direction.Right; }
        if (moveRef.x < 0) { playerdirection = direction.Left; }

    }

    void refreshAbilities()     //refreshes jumps & dashes etc.
    {
        Jumps = 2;
        dashused = false;
    }

    //checks ground state
    void OnTriggerEnter2D(Collider2D collision)     //checks if player is grounded
    {
        if (collision.tag == "Platform")
        {
            playerMoveState = moveState.Grounded;
            isGrounded = true;
            refreshAbilities();
        }
    }

    //matches velocities and variables to current movestate, eg falling speed
    void matchMoveState()
    {
        if(rigidRef.velocity.y < 0)
        {
            //playerMoveState = moveState.Falling;
        }

        switch (playerMoveState)    //movestates: Grounded, Jumping, Falling, Dashing, Gliding, Walled, Other
        {
            case (moveState.Grounded):
                rigidRef.gravityScale = 0;
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, 0);
                break;

            case (moveState.Jumping):
                //rigidRef.gravityScale = 10;
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, rigidRef.velocity.y - 1);
                break;

            case (moveState.Falling):
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, rigidRef.velocity.y - 1);
                break;

            case (moveState.Dashing):
                break;

            case (moveState.Gliding):
                break;

            case (moveState.Walled):
                break;

            case (moveState.Other):
                break;
        }
    }
}
