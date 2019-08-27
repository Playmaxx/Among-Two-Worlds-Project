using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour    //manages central aspects such as world shifting, active scripts etc.
{

    public static GameManager GMInstance;
    Player playerRef;

    public int playerMask = 1 << 8;
    public int enemyMask = 1 << 9;
    public int platformMask = 1 << 10;

    public enum dimension { None, Light, Dark }
    public dimension currentdim;
    public enum level { Tutorial, Eingang, Haupthalle, Keller, Türme, Gang, Boss }
    public level currentlvl;
    public static int score = 0;

    public int keys = 0;
    public bool TowerLever = false;
    public bool DungeonLever = false;
    [HideInInspector]
    public string OriginScene;

    public int respawnX;
    public int respawnY;

    private void Awake()
    {
        currentdim = dimension.Light;
        currentlvl = level.Tutorial;
        playerRef = GameObject.FindObjectOfType<Player>();

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
            updateDimensions();
        }
    }

    void updateDimensions()     //updates dimensions for all platforms and backgrounds
    {
        Platform[] AllPlatforms = FindObjectsOfType(typeof(Platform)) as Platform[];
        foreach (Platform item in AllPlatforms)
        {
            item.updateDimensions();
        }
        Background[] AllBackgrounds = FindObjectsOfType(typeof(Background)) as Background[];
        foreach (Background item in AllBackgrounds)
        {
            item.updateDimensions();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoad;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoad;
    }

    void onSceneLoad(Scene loadedScene, LoadSceneMode mode)
    {
        Debug.Log(loadedScene.name);
        Debug.Log(OriginScene);
        switch (loadedScene.name)
        {
            case ("Level 1"):
                if (OriginScene == "Level 2")
                {
                    playerRef.transform.position = new Vector2(100, 28);
                }
                else
                {
                    playerRef.transform.position = Vector2.zero;
                }
                break;

            case ("Level 2"):
                if (OriginScene == "Level 1")
                {
                    playerRef.transform.position = new Vector2(0, 0);
                    Debug.Log("221");
                }
                if (OriginScene == "Level 3")
                {
                    playerRef.transform.position = new Vector2(39, -10);
                }
                if (OriginScene == "Level 4")
                {
                    playerRef.transform.position = new Vector2(39, 26);
                }
                if (OriginScene == "Level 5")
                {
                    playerRef.transform.position = new Vector2(41, 1);
                }
                break;

            case ("Level 3"):
                playerRef.transform.position = Vector2.zero;
                break;

            case ("Level 4"):
                playerRef.transform.position = Vector2.zero;
                break;

            case ("Level 5"):
                playerRef.transform.position = Vector2.zero;
                break;

            case ("Boss"):
                playerRef.transform.position = Vector2.zero;
                break;
        }
    }
}
