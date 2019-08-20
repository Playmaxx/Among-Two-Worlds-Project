using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text scoreText;
    public Canvas rootCanvas;
    [SerializeField]
    Player playerRef;

    // Use this for initialization
    void Start()
    {
        rootCanvas = GameObject.Find("Canvas_ScoreUI").GetComponent<Canvas>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        playerRef = GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "scoreSource")
        {
            GameManager.score++;
            scoreText.text = "Score: " + GameManager.score;
        }
    }
}
