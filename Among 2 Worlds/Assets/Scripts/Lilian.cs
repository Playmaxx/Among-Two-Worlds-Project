using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilian : MonoBehaviour      //manages abilities for Lilian
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
            if (Input.GetKey(KeyCode.A) || Input.GetAxis("MoveHorizontal") < 0)
            {
                playerRef.rigidRef.velocity = new Vector2(-playerRef.moveSpeed, playerRef.rigidRef.velocity.y);
                playerRef.playerdirection = Player.direction.Left;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetAxis("MoveHorizontal") > 0)
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") && playerRef.playerMoveState != Player.moveState.Walled)
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
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Dash")) && playerRef.dashused == false && playerRef.playerMoveState != Player.moveState.Walled)
        {
            playerRef.dashused = true;
            playerRef.playerMoveState = Player.moveState.Dashing;
            playerRef.currentDashTime = playerRef.dashtime;
        }

        if (playerRef.currentDashTime > 0)
        {
            switch (playerRef.playerdirection)
            {
                case (Player.direction.Left):
                    playerRef.rigidRef.velocity = new Vector2(+playerRef.dashspeed, 0);
                    break;

                case (Player.direction.Right):
                    playerRef.rigidRef.velocity = new Vector2(-playerRef.dashspeed, 0);
                    break;
            }
        }
        else if (playerRef.playerMoveState == Player.moveState.Dashing)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, playerRef.playerheight / 2, GameManager.GMInstance.platformMask))
            {
                playerRef.playerMoveState = Player.moveState.Grounded;
                playerRef.refreshAbilities();
            }
            else
            {
                playerRef.playerMoveState = Player.moveState.Falling;
            }
        }
    }

    public void glide()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Jump")) && playerRef.rigidRef.velocity.y < 0 && playerRef.playerMoveState == Player.moveState.Falling)
        {
            if (playerRef.currentGlideTime < playerRef.maxGlideTime)
            {
                playerRef.playerMoveState = Player.moveState.Gliding;
            }
            else
            {
                playerRef.playerMoveState = Player.moveState.Falling;
            }
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump")) && playerRef.playerMoveState == Player.moveState.Gliding)
        {
            playerRef.playerMoveState = Player.moveState.Falling;
        }
    }

    public void wallaction()
    {
        if ((Physics2D.Raycast(transform.position, Vector2.left, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask)))
        {
            if (playerRef.playerMoveState != Player.moveState.Grounded && playerRef.playerMoveState != Player.moveState.Jumping)
            {
                playerRef.lastWallDirection = Player.wallDirection.Left;
                playerRef.playerMoveState = Player.moveState.Walled;
            }
        }
        if ((Physics2D.Raycast(transform.position, Vector2.right, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask)))
        {
            if (playerRef.playerMoveState != Player.moveState.Grounded && playerRef.playerMoveState != Player.moveState.Jumping)
            {
                playerRef.lastWallDirection = Player.wallDirection.Right;
                playerRef.playerMoveState = Player.moveState.Walled;
            }
        }
        if (!Physics2D.Raycast(transform.position, Vector2.right, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask))
        {
            if (!Physics2D.Raycast(transform.position, Vector2.left, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask))
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
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, playerRef.playerwidth, GameManager.GMInstance.platformMask))
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            {
                if (Input.GetAxis("MoveHorizontal") > 0)
                {
                    playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, playerRef.jumpforce);
                    playerRef.playerMoveState = Player.moveState.Walljumping;
                }

            }
        }
        if (Physics2D.Raycast(transform.position, Vector2.right, playerRef.playerwidth, GameManager.GMInstance.platformMask))
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            {
                if (Input.GetAxis("MoveHorizontal") < 0)
                {
                    playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, playerRef.jumpforce);
                    playerRef.playerMoveState = Player.moveState.Walljumping;
                }
            }
        }
    }
}
