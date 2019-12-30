using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    private int score = Player.CalculateScore();

    void Awake()
    {
        scoreText.text = "Score: " + score;
    }
}
