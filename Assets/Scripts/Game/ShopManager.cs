using System.Collections;
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

        foreach(CardObject sortedCard in CardManager.Instance.sortedLoadedCards)
        {
            ShopCard shopCard = Instantiate(shopCardInstance, shopCardContainer).GetComponent<ShopCard>();
            shopCard.SetCard(sortedCard);
        }
    }

    public void GoBackButton()
    {
        StartCoroutine(GoBackButtonCoroutine());
    }

    public void MoreCoinsButton()
    {

    }

    private IEnumerator GoBackButtonCoroutine()
    {
        Fade.Instance.PlayFadeOut();
        yield return new WaitForSeconds(Fade.Instance.duration);
        SceneManager.LoadScene("Main Menu");
    }
}
