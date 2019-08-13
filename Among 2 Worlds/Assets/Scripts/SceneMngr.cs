using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{
    public enum targetScene { Eingang, Haupthalle, Keller, Türme, Gang, Boss }
    public targetScene t_Scene;
    public enum currentScene { Eingang, Haupthalle, Keller, Türme, Gang, Boss }
    public currentScene c_Scene;

    bool colliding = false;

    [SerializeField]
    Player playerRef;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliding = false;
    }

    private void Update()
    {
        if (colliding && Input.GetKey(KeyCode.Q))
        {
            switch (t_Scene)
            {
                case (targetScene.Eingang):
                    GameManager.GMInstance.currentlvl = GameManager.level.Eingang;
                    SceneManager.LoadScene("Level 1");
                    if (c_Scene == currentScene.Haupthalle)
                    {
                        playerRef.transform.position = new Vector2(70, 23.28661f);
                        Debug.Log("transform on dis position plz");
                    }
                    break;
                case (targetScene.Haupthalle):
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
        }
    }

}
