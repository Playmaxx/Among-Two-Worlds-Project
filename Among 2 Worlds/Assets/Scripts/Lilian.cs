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

    public void movement(Vector2 moveVector)     //handles basic movement
    {
        playerRef.rigidRef.velocity = (new Vector2(moveVector.x * playerRef.speedMultiplier, playerRef.rigidRef.velocity.y));
    }

    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (playerRef.Jumps)
            {
                case (2):
                    if (playerRef.isGrounded == true)
                    {
                        playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, 0);
                        playerRef.rigidRef.velocity += Vector2.up * playerRef.jumpforce;
                        playerRef.Jumps -= 2;
                    }
                    break;

                default:
                    break;
            }
        }

        if (playerRef.rigidRef.velocity.y < 0)
        {
            playerRef.rigidRef.velocity += Vector2.up * Physics2D.gravity.y * (playerRef.gravityMultiplier) * Time.deltaTime;
        }

    }

    public void dash(Vector2 moveVector)     //dash
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift) && playerRef.dashused == false)        //press shift + a||d to trigger
            {
                playerRef.rigidRef.velocity = Vector2.zero;
                playerRef.rigidRef.velocity = new Vector2(-moveVector.x * playerRef.dashspeed, 0);
                //playerRef.dashused = true;
            }
        }
    }

    public void glide()     //glide
    {
        if (Input.GetKey(KeyCode.Space) && playerRef.isGrounded == false && playerRef.rigidRef.velocity.y < 0)
        {
            playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, -playerRef.glidespeed);

        }
    }

    public void wallaction()
    {
        if (playerRef.isWalled == true)
        {
            playerRef.rigidRef.velocity = new Vector2(playerRef.rigidRef.velocity.x, -playerRef.wallSlideSpeed);
        }
    }
}
