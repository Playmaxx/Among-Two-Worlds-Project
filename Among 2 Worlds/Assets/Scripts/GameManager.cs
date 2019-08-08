using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour    //manages central aspects such as world shifting, active scripts etc.
{

    public static GameManager GMInstance;
    Player playerRef;

    public int playerMask = 1 << 8;
    public int enemyMask = 1 << 9;
    public int platformMask = 1 << 10;

    public enum dimension { None, Light, Dark }
    public dimension currentdim;
    public enum level {Tutorial, Eingang, Haupthalle, Keller, Türme, Boss}
    public level currentlvl;

    private void Awake()
    {
        currentdim = dimension.Light;
        currentlvl = level.Tutorial;
        playerRef = GetComponent<Player>();

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
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Worldshift"))
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

    void updateDimensions()     //updates dimensions for all platforms and backgrounds
    {
        Platform[] AllPlatforms = FindObjectsOfType(typeof(Platform)) as Platform[];
        foreach(Platform item in AllPlatforms)
        {
            //item.gameObject.updateDimensions();
        }
    }
}
