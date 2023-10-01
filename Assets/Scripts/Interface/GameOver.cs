using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverScore, gameOverCoins;
    [SerializeField] private float speed;

    public int currentScore, currentCoins;
    public int score, coins;

    private bool lerp = false;
    private float time = 0f;
    private float duration = 3f;

    public void TriggerGameOver(int score, int coins)
    {
        this.score = score;
        this.coins = coins;
        this.lerp = true;
    }

    private void Update() 
    {
        if(lerp)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            currentScore = Mathf.RoundToInt(Mathf.SmoothStep(currentScore, score, t));
            currentCoins = Mathf.RoundToInt(Mathf.SmoothStep(currentCoins, coins, t));
            gameOverScore.text = currentScore.ToString();
            gameOverCoins.text = currentCoins.ToString();
        }
    }
}
