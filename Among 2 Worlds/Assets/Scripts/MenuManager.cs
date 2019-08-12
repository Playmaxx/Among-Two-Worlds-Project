using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadCredits()
    {
        Debug.Log("oi");
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
