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
