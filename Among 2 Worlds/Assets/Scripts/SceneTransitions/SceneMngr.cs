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
        if (collision.tag == "Player")
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
            Debug.Log(playerRef.health);
        }

        if (sceneIsSwitching == true)
            switch (t_Scene)
            {
                case (targetScene.Eingang):
                    GameManager.GMInstance.currentlvl = GameManager.level.Eingang;
                    SceneManager.LoadScene("Level 1");
                        PlayerIsComingBack();
                    break;
                case (targetScene.Haupthalle):
                    PlayerIsSwitchingScene();
                    GameManager.GMInstance.currentlvl = GameManager.level.Haupthalle;
                    SceneManager.LoadScene("Level 2");
                    //playerRef.transform.position = new Vector2(0.15f, 0.8799992f);
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
                    GameManager.GMInstance.currentlvl = GameManager.level.Gang;
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
        PlayerPrefs.SetFloat("X-Eingang", playerRef.transform.position.x);
        PlayerPrefs.SetFloat("Y-Eingang", playerRef.transform.position.y);
        Debug.Log(PlayerPrefs.GetFloat("X-Eingang"));
        Debug.Log(PlayerPrefs.GetFloat("Y-Eingang"));
    }
    void PlayerIsComingBack()
    {
        Debug.Log(PlayerPrefs.GetFloat("X-Eingang"));
        Debug.Log(PlayerPrefs.GetFloat("Y-Eingang"));
        playerRef.transform.position = new Vector2(PlayerPrefs.GetFloat("X-Eingang"), PlayerPrefs.GetFloat("Y-Eingang")); //vll ned die richtige schreibweise
        Debug.Log(playerRef.transform.position);
    }

}
