using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static int score;
    public Text scoreText;
    public Canvas rootCanvas;

    // Use this for initialization
    void Start()
    {
        rootCanvas = GameObject.Find("Canvas_ScoreUI").GetComponent<Canvas>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        score = 0;
        DontDestroyOnLoad(this.rootCanvas);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            score++;
            scoreText.text = "Score: " + score;
            Destroy(this.gameObject);
        }
    }
}
