﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour    //manages central aspects such as world shifting, active scripts etc.
{

    public static GameManager GMInstance;

    public enum dimension { None, Light, Dark }
    public dimension currentdim;
    public enum level {Tutorial, Eingang, Haupthalle, Keller, Türme, Boss}
    public level currentlvl;

    private void Awake()
    {
        currentdim = dimension.Light;
        currentlvl = level.Tutorial;

        if (GMInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            GMInstance = this;
        }
        else if (GMInstance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switchDimension();
    }

    public void switchDimension()      //reverses current dimension and switches active scripts
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (currentdim)
            {
                case (dimension.Light):
                    currentdim = dimension.Dark;
                    break;

                case (dimension.Dark):
                    currentdim = dimension.Light;
                    break;
            }
        }
    }
}
