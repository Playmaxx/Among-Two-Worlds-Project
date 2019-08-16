﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour     //manages aspects of the player that apply to both Gale and Lilian
{
    public enum direction { Left, Right }       // variables
    public direction playerdirection;
    public enum moveState { Grounded, Jumping, Falling, Dashing, Gliding, Walled, Other }
    public moveState playerMoveState;

    public float playerheight = 2;
    public float playerwidth = 1;
    public int Jumps = 2;
    public float health;
    public float moveSpeed = 7.5f;
    public int jumpforce = 20;
    public int dashspeed = 300;
    public int dashtime = 10;
    public bool dashused = false;
    public int glidespeed = 2;
    public float wallSlideSpeed = 0.1f;
    public float playerGravity = 10;
    public float maxGlideTime = 5;
    public float currentGlideTime = 0;
    public float maxDashTime = 1;
    public float currentDashTime = 0;

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
        playerMoveState = moveState.Falling;
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
                lilianRef.movement();
                lilianRef.jump();
                //lilianRef.dash();
                lilianRef.glide();
                lilianRef.wallaction();
                break;
            case (GameManager.dimension.Dark):
                galeRef.movement();
                galeRef.jump();
                galeRef.dash();
                galeRef.glide();
                galeRef.wallaction();
                break;

        }


        //tests
        Debug.Log(playerMoveState);
        Debug.DrawRay(transform.position, Vector2.down * playerheight / 2, Color.green);
        Debug.DrawRay(transform.position, Vector2.left * playerwidth / 2, Color.green);
        Debug.DrawRay(transform.position, Vector2.right * playerwidth / 2, Color.green);

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
        if(playerMoveState == moveState.Gliding)
        {
            currentGlideTime -= 1 * Time.deltaTime;
        }
        if (playerMoveState == moveState.Dashing)
        {
            currentDashTime -= 1 * Time.deltaTime;
        }
    }

    void refreshAbilities()     //refreshes jumps & dashes etc.
    {
        Jumps = 2;
        dashused = false;
    }

    //checks ground state
    void OnTriggerEnter2D(Collider2D collision)     //checks if player is grounded
    {
        if (collision.tag == "Platform" && playerMoveState != moveState.Dashing)
        {
            playerMoveState = moveState.Grounded;
            refreshAbilities();
        }
        if (collision.tag == "Enemy" && playerMoveState == moveState.Dashing)
        {
            //aa
        }
    }
    //checks if player walked off edge
    void OnTriggerExit2D(Collider2D collision)     //checks if player is grounded
    {
        if (collision.tag == "Platform" && playerMoveState != moveState.Jumping)
        {
            playerMoveState = moveState.Falling;
        }
    }

    void matchMoveState()
    {
        if (rigidRef.velocity.y < 0 && !Physics2D.Raycast(transform.position, Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask) && playerMoveState != Player.moveState.Gliding)
        {
            playerMoveState = moveState.Falling;
        }

        if (playerMoveState == moveState.Gliding && Physics2D.Raycast(transform.position, Vector2.down, playerheight / 2, GameManager.GMInstance.platformMask))
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
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, rigidRef.velocity.y - 1);
                break;

            case (moveState.Dashing):
                break;

            case (moveState.Gliding):
                break;

            case (moveState.Walled):
                Debug.Log("Wall test");
                rigidRef.velocity = new Vector2(rigidRef.velocity.x, -glidespeed);
                break;

            case (moveState.Other):
                break;
        }
    }

    void Damage(int amount)
    {
        health -= amount;
    }
    void Heal(int amount)
    {
        health += amount;
    }
}
