using System.Collections;
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

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && Input.GetAxis("MoveHorizontal") == 0)
            {
                playerRef.rigidRef.velocity = new Vector2(0, playerRef.rigidRef.velocity.y);
            }
        }
    }

    public void jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")))
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == false)
            {
                if (Physics2D.Raycast(transform.position, Vector2.right, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == false)
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
        }
    }

    public void dash()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Dash")) && playerRef.dashused == false)
        {
            playerRef.playerMoveState = Player.moveState.Dashing;
            if (playerRef.playerdirection == Player.direction.Left)
            {
                float dashend = playerRef.transform.position.x - playerRef.dashdistance;
                for (float i = playerRef.transform.position.x; i > dashend; i--)
                {
                    playerRef.rigidRef.velocity = new Vector2(-playerRef.dashspeed, 0);
                }
                playerRef.dashused = true;
            }
            if (playerRef.playerdirection == Player.direction.Right)
            {
                float dashend = playerRef.transform.position.x + playerRef.dashdistance;
                for (float i = playerRef.transform.position.x; i < dashend; i++)
                {
                    playerRef.rigidRef.velocity = new Vector2(playerRef.dashspeed, 0);
                }
                playerRef.dashused = true;
            }
            playerRef.playerMoveState = Player.moveState.Falling;
        }
        //GetComponent<SFXManager>().playDashSound();
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
        if ((Physics2D.Raycast(transform.position, Vector2.left, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == true))
        {
            if (playerRef.playerMoveState != Player.moveState.Grounded && playerRef.playerMoveState != Player.moveState.Jumping)
            {
                playerRef.playerMoveState = Player.moveState.Walled;
                playerRef.playerWallSide = Player.wallSide.Left;
            }
        }
        if ((Physics2D.Raycast(transform.position, Vector2.right, playerRef.playerwidth / 2, GameManager.GMInstance.platformMask) == true))
        {
            if (playerRef.playerMoveState != Player.moveState.Grounded && playerRef.playerMoveState != Player.moveState.Jumping)
            {
                playerRef.playerMoveState = Player.moveState.Walled;
                playerRef.playerWallSide = Player.wallSide.Right;
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
                if (playerRef.playerWallSide == Player.wallSide.Left)
                {
                    if (Input.GetKey(KeyCode.D) || Input.GetAxis("MoveHorizontal") > 0)
                    {
                        playerRef.playerMoveState = Player.moveState.Jumping;
                        playerRef.rigidRef.velocity = new Vector2(0, playerRef.jumpforce);
                    }
                }
                if (playerRef.playerWallSide == Player.wallSide.Right)
                {
                    if (Input.GetKey(KeyCode.A) || Input.GetAxis("MoveHorizontal") < 0)
                    {
                        playerRef.playerMoveState = Player.moveState.Jumping;
                        playerRef.rigidRef.velocity = new Vector2(0, playerRef.jumpforce);
                    }
                }
            }
        }
    }
}
