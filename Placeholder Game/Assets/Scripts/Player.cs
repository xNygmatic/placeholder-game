using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour

{
    public static int livesAmount = 3;
    public static int coinsAmount = 0;
    public static int gemsAmount = 0;
    public static int stompsAmount = 0;

    public Text coinText;
    public Text livesText;
    public Text killsText;



    void Update()
    {
        if (livesAmount >= 0) //only doing this so it doesn't show -1 when you die with 0 extra lives left
        {
            livesText.text = "Lives: " + livesAmount;
        }
        coinText.text = "Coins: " + coinsAmount;
        killsText.text = "Kills: " + stompsAmount;
    }

    public static void ResetValues()
    {
        PlayerMovement.hitCheckpoint = false;
        coinsAmount = 0;
        livesAmount = 3;
        gemsAmount = 0;
        stompsAmount = 0;
    }

    public static int CalculateScore()
    {
        int score = 0;
        score += coinsAmount * 5;
        score += stompsAmount * 10;
        
        if (livesAmount == 3) {
            score += 100;
        } else {
            score -= livesAmount * 100;
        }

        return score;
    }
}
