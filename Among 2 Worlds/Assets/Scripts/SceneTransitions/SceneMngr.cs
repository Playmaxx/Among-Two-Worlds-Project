using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{
    public enum targetScene { Eingang, Haupthalle, Keller, Türme, Gang, Boss }
    public targetScene t_Scene;
    Scene currentScene;
    string sceneName;

    bool colliding = false;
    bool sceneIsSwitching = false;
    
    [SerializeField]
    Player playerRef;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        //DontDestroyOnLoad(playerRef);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SceneSwitcher")
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SceneSwitcher")
        {
            colliding = false;
        }
    }

    private void Update()
    {
        if (colliding && Input.GetKey(KeyCode.Q))
        {
            sceneIsSwitching = true;
            Debug.Log(sceneIsSwitching);
        }

        if (sceneIsSwitching == true)
            switch (t_Scene)
            {
                case (targetScene.Eingang):
                    GameManager.GMInstance.currentlvl = GameManager.level.Eingang;
                    SceneManager.LoadScene("Level 1");
                    if (currentScene.name == "Level 1")
                    {
                        PlayerIsComingBack();
                    }
                    break;
                case (targetScene.Haupthalle):
                    PlayerIsSwitchingScene();
                    GameManager.GMInstance.currentlvl = GameManager.level.Haupthalle;
                    SceneManager.LoadScene("Level 2");
                    break;
                case (targetScene.Keller):
                    GameManager.GMInstance.currentlvl = GameManager.level.Keller;
                    SceneManager.LoadScene("Level 3");
                    break;
                case (targetScene.Türme):
                    GameManager.GMInstance.currentlvl = GameManager.level.Türme;
                    SceneManager.LoadScene("Level 4");
                    break;
                case (targetScene.Gang):
                    //GameManager.GMInstance.currentlvl = GameManager.level.Gang;
                    SceneManager.LoadScene("Level 5");
                    break;
                case (targetScene.Boss):
                    GameManager.GMInstance.currentlvl = GameManager.level.Boss;
                    SceneManager.LoadScene("Boss");
                    break;
            }
        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log(PlayerPrefs.GetFloat("X"));
            Debug.Log(PlayerPrefs.GetFloat("Y"));
            Debug.Log(playerRef.transform.position);
        }
    }

    void PlayerIsSwitchingScene()
    {
        PlayerPrefs.SetFloat("X", playerRef.transform.position.x);
        PlayerPrefs.SetFloat("Y", playerRef.transform.position.y);
        Debug.Log(PlayerPrefs.GetFloat("X"));
        Debug.Log(PlayerPrefs.GetFloat("Y"));
    }
    void PlayerIsComingBack()
    {
        Debug.Log(PlayerPrefs.GetFloat("X"));
        Debug.Log(PlayerPrefs.GetFloat("Y"));
        playerRef.transform.position = new Vector2(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y")); //vll ned die richtige schreibweise
        Debug.Log(playerRef.transform.position);
    }

}
