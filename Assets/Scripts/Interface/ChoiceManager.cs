using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public List<CardObject> initialCards;
    public int maxCards;

    public GameObject choiceCardInstance;
    public Transform container;
    public TMP_Text counterText;
    public GameObject confirmButton;

    public List<CardObject> selectedCards = new List<CardObject>();

    private void Start()
    {
        foreach(CardObject card in initialCards)
        {
            ChoiceCard cardInstance = Instantiate(choiceCardInstance, container).GetComponent<ChoiceCard>();
            cardInstance.choiceManager = this;
            cardInstance.SetCard(card);
        }

        UpdateCounter();
    }

    public void UpdateCounter()
    {
        counterText.text = selectedCards.Count.ToString() + "/" + maxCards.ToString();
        confirmButton.SetActive(selectedCards.Count >= maxCards);
    }

    public void ConfirmButtonDown()
    {
        StartCoroutine(ConfirmButtonDownCoroutine());
    }

    private IEnumerator ConfirmButtonDownCoroutine()
    {
        Fade.Instance.PlayFadeOut();

        yield return new WaitForSeconds(Fade.Instance.duration);

        Fade.Instance.PlayFadeIn();

        ResourceManager.Instance.playerEarnedCards.Clear();
        foreach(CardObject card in selectedCards)
        {
            ResourceManager.Instance.playerEarnedCards.Add(card.id);
        }

        ResourceManager.Instance.Save();
        Destroy(gameObject);
    }
}
