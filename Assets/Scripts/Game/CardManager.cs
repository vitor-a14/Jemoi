using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;    
    public Dictionary<int, CardObject> cards = new Dictionary<int, CardObject>();

    void Awake()
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
        return cards[Random.Range(0, cards.Count)];
    }

    public CardObject GetEnemyCard()
    {
        return cards[Random.Range(0, cards.Count)];
    }
}
