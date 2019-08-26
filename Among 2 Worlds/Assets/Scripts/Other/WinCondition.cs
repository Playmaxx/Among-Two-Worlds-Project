﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    private Player PlayerRef;


    private void Awake()
    {
        PlayerRef = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerRef.playerMoveState = Player.moveState.Grounded;
            PlayerRef.playerMoveState = Player.moveState.Other;
        }
    }


}
