using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;  

    public GameObject shopCardInstance;
    public Transform shopCardContainer;

    public ShopCard currentShopCard;

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

    private void Start()
    {
        GetShopCards();
        ShopCounter.Instance.SetValue(ResourceManager.Instance.playerEarnedCards.Count);
        CoinText.Instance.UpdateCoinText(ResourceManager.Instance.coins);
    }

    private void GetShopCards()
    {
        if(ResourceManager.Instance == null)
        {
            Debug.LogError("Resource Manager is not instanciated!");
            return;
        }

        for(int i = 0; i < CardManager.Instance.cards.Count; i++)
        {
            CardObject card = CardManager.Instance.cards[i];
            ShopCard shopCard = Instantiate(shopCardInstance, shopCardContainer).GetComponent<ShopCard>();

            shopCard.SetCard(card);
        }
    }

    public void GoBackButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void MoreCoinsButton()
    {

    }
}
