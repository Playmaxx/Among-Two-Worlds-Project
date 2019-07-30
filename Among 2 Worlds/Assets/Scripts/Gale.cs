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
                playerRef.transform.Translate(Vector2.left * playerRef.speedMultiplier);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerRef.transform.Translate(Vector2.right * playerRef.speedMultiplier);
            }
            if (!Input.anyKey)
            {
                Vector2 moveVector = new Vector2(Input.GetAxis("MoveHorizontal"), 0);
                playerRef.transform.Translate(moveVector * playerRef.speedMultiplier);
            }
        }
    }

    public void jump()
    {
        throw new System.NotImplementedException();
    }

    public void dash()
    {
        throw new System.NotImplementedException();
    }

    public void glide()
    {
        throw new System.NotImplementedException();
    }

    public void wallaction()
    {
        throw new System.NotImplementedException();
    }
}
