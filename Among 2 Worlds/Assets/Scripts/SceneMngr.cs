using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{
    public enum targetScene { Haupthalle, Keller, Türme, Boss, Gang }
    public targetScene t_Scene;
    [SerializeField]
    Player playerRef;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (t_Scene)
            {
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
