using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;  

    public int coins;
    public int maxScore;
    public List<int> playerEarnedCards = new List<int>();

    public GameObject newPlayerScreen;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } 
        else 
        {
            Destroy(gameObject);
            return;
        }

        Application.targetFrameRate = 75;
        Physics.simulationMode = SimulationMode.Script;
        
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        coins = Mathf.Clamp(coins, 0, 99999);
    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        maxScore = Mathf.Clamp(maxScore, 0, 99999);
    }

    public void AddEarnedCard(int cardId)
    {
        playerEarnedCards.Add(cardId);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("maxScore", maxScore);
        
        string earnedCards = "";
        foreach(int card in playerEarnedCards)
        {
            earnedCards += card.ToString() + " ";
        }

        try 
        {
            earnedCards = earnedCards.Substring(0, earnedCards.Length - 1);
        } 
        catch
        {
            earnedCards = "";
        }

        PlayerPrefs.SetString("earnedCards", earnedCards);
    }

    public void Load()
    {
        coins = PlayerPrefs.GetInt("coins"); 
        maxScore = PlayerPrefs.GetInt("maxScore");

        int cardValue;
        string[] earnedCards = PlayerPrefs.GetString("earnedCards").Split(' ');
        playerEarnedCards = new List<int>();

        foreach(string card in earnedCards)
        {
            if(card != "" && card != null && card != " ")
            {
                int.TryParse(card, out cardValue);
                playerEarnedCards.Add(cardValue);
            }
        }

        //started a new game (improve this in the future)
        if((playerEarnedCards.Count <= 1 || playerEarnedCards == null) && newPlayerScreen != null)
        {
            newPlayerScreen.SetActive(true);
        }
    }
}
