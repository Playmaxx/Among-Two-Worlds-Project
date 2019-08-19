using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static int score;
    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        score = 0;
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
