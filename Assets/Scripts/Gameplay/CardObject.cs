using UnityEngine;

public enum Rarity
{
    NORMAL,
    UNCOMMON,
    RARE,
    EPIC
}

[CreateAssetMenu(fileName = "New Emoji", menuName = "Emoji/New Emoji")]
public class CardObject : ScriptableObject {
    public int id;
    public new string name;
    public int price;
    public Rarity rarity;
    public Sprite artwork;
    public string weakAgainst;
}

