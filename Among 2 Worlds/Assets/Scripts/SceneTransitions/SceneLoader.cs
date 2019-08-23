using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum targetScene { Eingang, Haupthalle, Keller, Türme, Gang, Boss }
    public targetScene t_Scene;

    public int targetX;
    public int targetY;

    bool intrigger = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            intrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            intrigger = false;
        }
    }

    void FixedUpdate()
    {
        if (intrigger == true && (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Interact")))
        {
            loadScene();
        }
    }

    void loadScene()
    {
        GameManager.GMInstance.respawnX = targetX;
        GameManager.GMInstance.respawnY = targetY;
        Debug.Log(GameManager.GMInstance.respawnX);
        Debug.Log(GameManager.GMInstance.respawnY);

        switch (t_Scene)
        {
            case (targetScene.Eingang):
                GameManager.GMInstance.currentlvl = GameManager.level.Eingang;
                SceneManager.LoadScene("Level 1");
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
                GameManager.GMInstance.currentlvl = GameManager.level.Gang;
                SceneManager.LoadScene("Level 5");
                break;

            case (targetScene.Boss):
                GameManager.GMInstance.currentlvl = GameManager.level.Boss;
                SceneManager.LoadScene("Boss");
                break;
        }
    }
}
