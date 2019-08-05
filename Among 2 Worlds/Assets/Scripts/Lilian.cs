using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilian : MonoBehaviour, IChar        //manages abilities for Lilian
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

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && Input.GetAxis("MoveHorizontal") == 0)
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
                    break;
                default:
                    break;
            }
        }
    }

    public void dash()
    {
        throw new System.NotImplementedException();
    }

    public void glide()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Jump")) && playerRef.rigidRef.velocity.y < 0 && playerRef.playerMoveState == Player.moveState.Falling)
        {
            playerRef.playerMoveState = Player.moveState.Gliding;
            playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, -playerRef.glidespeed);
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump")) && playerRef.playerMoveState == Player.moveState.Gliding)
        {
            playerRef.playerMoveState = Player.moveState.Falling;
        }
    }

    public void wallaction()
    {
        if ((Physics2D.Raycast(transform.position, Vector2.left, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == true))
        {
            if (playerRef.playerMoveState != Player.moveState.Grounded && playerRef.playerMoveState != Player.moveState.Jumping)
            {
                playerRef.playerMoveState = Player.moveState.Walled;
            }
        }
        if ((Physics2D.Raycast(transform.position, Vector2.right, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == true))
        {
            if (playerRef.playerMoveState != Player.moveState.Grounded && playerRef.playerMoveState != Player.moveState.Jumping)
            {
                playerRef.playerMoveState = Player.moveState.Walled;
            }
        }
        if (Physics2D.Raycast(transform.position, Vector2.right, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == false)
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == false)
            {
                if (playerRef.playerMoveState == Player.moveState.Walled)
                {
                    playerRef.playerMoveState = Player.moveState.Falling;
                }
            }
        }
        if (playerRef.playerMoveState == Player.moveState.Walled)
        {
            playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, -playerRef.wallSlideSpeed);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            {
                playerRef.playerMoveState = Player.moveState.Jumping;
                playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, playerRef.jumpforce);
            }
        }
    }
}
