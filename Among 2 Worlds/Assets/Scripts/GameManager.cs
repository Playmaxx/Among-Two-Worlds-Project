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

    void onSceneLoad(Scene scene, LoadSceneMode mode)
    {
        playerRef = Player.PlayerInstance;
        Debug.Log(respawnX);
        Debug.Log(respawnY);
        playerRef.transform.position = new Vector2(respawnX, respawnY);
    }
}
