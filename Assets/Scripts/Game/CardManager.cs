using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;    
    public Dictionary<int, CardObject> cards = new Dictionary<int, CardObject>();
    private List<CardObject> playerCards = new List<CardObject>();

    void Start()
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

        CardObject[] loadedCards = Resources.LoadAll<CardObject>("Cards");
        foreach (CardObject card in loadedCards) 
        {
            cards[card.id] = card;

            if(ResourceManager.Instance.playerEarnedCards.Contains(card.id))
                playerCards.Add(card);
        }
    }

    public bool CheckWeakness(Card attacker, Card defender) 
    {
        CardObject attackerObject = attacker.GetComponent<Card>().cardObject;
        CardObject defenderObject = defender.GetComponent<Card>().cardObject;

        string[] defenderWeaknessesArray = defenderObject.weakAgainst.Split(' ');
        List<string> defenderWeaknesses = new List<string>(defenderWeaknessesArray);

        if(defenderWeaknesses.Contains(attackerObject.name)) 
        {
            return true;
        } 
        else if(defenderObject.name == attackerObject.name) 
        {
            int rand = Random.Range(0, 2);
            return rand > 0 ? true : false;
        } 
        else 
        {
            return false;
        }
    } 

    public CardObject GetPlayerCard() 
    {
        return playerCards[Random.Range(0, playerCards.Count)];
    }

    public CardObject GetEnemyCard()
    {
        return cards[Random.Range(0, cards.Count)];
    }
}
