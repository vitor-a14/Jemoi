using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct RarityColor
{
    public Rarity rarity;
    public Color color;
}

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;    

    public Dictionary<int, CardObject> cards = new Dictionary<int, CardObject>();
    public List<RarityColor> rarityColors = new List<RarityColor>();
    [HideInInspector] public List<CardObject> sortedLoadedCards = new List<CardObject>();

    private List<CardObject> playerCards = new List<CardObject>();

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
    }

    void Start()
    {
        sortedLoadedCards = Resources.LoadAll<CardObject>("Cards").OrderBy(card => card.price).ToList();
        UpdatePlayerDeck();
    }

    public void UpdatePlayerDeck()
    {
        foreach (CardObject card in sortedLoadedCards) 
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
        Rarity maxRarity = GameplayManager.Instance.maxEnemyCardRarity;
        List<CardObject> sortedEnemyCards = sortedLoadedCards.Where(card => card.rarity == maxRarity).ToList();
        return sortedEnemyCards[Random.Range(0, sortedEnemyCards.Count)];
    }
}
