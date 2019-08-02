﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gale : MonoBehaviour, IChar     //manages abilities for Gale
{
    Player playerRef;

    //Awake is called before Start
    void Awake()
    {
        playerRef = GetComponent<Player>();
    }

    public void movement()     //handles basic movement
    {
        if (playerRef.playerMoveState != Player.moveState.Other)
        {
            if (Input.GetKey(KeyCode.A))
            {
                playerRef.rigidRef.velocity = new Vector2(-playerRef.moveSpeed, playerRef.rigidRef.velocity.y);
                playerRef.playerdirection = Player.direction.Left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerRef.rigidRef.velocity = new Vector2(playerRef.moveSpeed, playerRef.rigidRef.velocity.y);
                playerRef.playerdirection = Player.direction.Right;
            }

            if (Input.GetAxis("MoveHorizontal") < 0)
            {
                playerRef.rigidRef.velocity = new Vector2(-playerRef.moveSpeed, playerRef.rigidRef.velocity.y);
                playerRef.playerdirection = Player.direction.Left;
            }
            if (Input.GetAxis("MoveHorizontal") > 0)
            {
                playerRef.rigidRef.velocity = new Vector2(playerRef.moveSpeed, playerRef.rigidRef.velocity.y);
                playerRef.playerdirection = Player.direction.Right;
            }

            if (!Input.anyKey && Input.GetAxis("MoveHorizontal") == 0)
            {
                playerRef.rigidRef.velocity = new Vector2(0, playerRef.rigidRef.velocity.y);
            }
        }
    }

    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            switch (playerRef.Jumps)
            {
                case (2):
                    if (playerRef.playerMoveState == Player.moveState.Grounded)
                    {
                        playerRef.playerMoveState = Player.moveState.Jumping;
                        playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, playerRef.jumpforce);
                        playerRef.Jumps--;
                    }
                    else
                    {
                        playerRef.playerMoveState = Player.moveState.Jumping;
                        playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, playerRef.jumpforce);
                        playerRef.Jumps -= 2;
                    }
                    break;
                case (1):
                    playerRef.playerMoveState = Player.moveState.Jumping;
                    playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, playerRef.jumpforce);
                    playerRef.Jumps--;
                    break;
                default:
                    break;
            }
        }
    }

    public void dash()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Dash"))
        {
            playerRef.playerMoveState = Player.moveState.Dashing;
            if(playerRef.playerdirection == Player.direction.Left)
            {
                playerRef.rigidRef.velocity = new Vector2(-playerRef.dashspeed, 0);
            }
            if (playerRef.playerdirection == Player.direction.Right)
            {
                playerRef.rigidRef.velocity = new Vector2(playerRef.dashspeed, 0);
            }
        }
    }

    public void glide()
    {
        if (playerRef.playerMoveState == Player.moveState.Gliding)
        {
            playerRef.playerMoveState = Player.moveState.Falling;
        }
    }

    public void wallaction()
    {
        if((Physics2D.Raycast(transform.position, Vector2.left, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == true ) && playerRef.playerMoveState != Player.moveState.Grounded)
        {
            playerRef.playerMoveState = Player.moveState.Walled;
        }
        if ((Physics2D.Raycast(transform.position, Vector2.right, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == true) && playerRef.playerMoveState != Player.moveState.Grounded)
        {
            playerRef.playerMoveState = Player.moveState.Walled;
        }
    }
}
