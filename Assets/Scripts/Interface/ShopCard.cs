using TMPro;
using UnityEngine;

public class ShopCard : Card
{
    public TMP_Text priceText;
    public GameObject purchasedIcon, confirmIcon;

    public bool alreadyPurchased = false;
    public bool confirm = false;

    public override void SetCard(CardObject cardObject)
    {
        base.SetCard(cardObject);
        priceText.text = cardObject.price.ToString();

        if(ResourceManager.Instance.playerEarnedCards.Contains(cardObject.id))
        {
            alreadyPurchased = true;
            priceText.gameObject.SetActive(false);
            purchasedIcon.SetActive(true);
        }
    }

    protected override void OnButtonDown()
    {
        if(alreadyPurchased) return;

        base.OnButtonDown();
    }

    protected override void OnButtonUp()
    {
        if(alreadyPurchased) return;

        if(ShopManager.Instance.currentShopCard != null && ShopManager.Instance.currentShopCard != this)
        {
            ShopManager.Instance.currentShopCard.confirmIcon.SetActive(false);
            ShopManager.Instance.currentShopCard.confirm = false;
        }

        base.OnButtonUp();

        if(!confirm)
        {
            ShopManager.Instance.currentShopCard = this;
            confirmIcon.SetActive(true);
            confirm = true;
        }
        else
        {
            if(ResourceManager.Instance.coins >= cardObject.price)
            {
                ResourceManager.Instance.AddCoins(-cardObject.price);
                ResourceManager.Instance.AddEarnedCard(cardObject.id);
                priceText.gameObject.SetActive(false);
                confirmIcon.SetActive(false);
                purchasedIcon.SetActive(true);
                alreadyPurchased = true;

                ResourceManager.Instance.Save();
                CoinText.Instance.UpdateCoinText(ResourceManager.Instance.coins);
                ShopCounter.Instance.SetValue(ResourceManager.Instance.playerEarnedCards.Count);
            }
        }
    }
}
