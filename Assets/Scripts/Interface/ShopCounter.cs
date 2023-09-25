using TMPro;
using UnityEngine;

public class ShopCounter : MonoBehaviour
{
    public static ShopCounter Instance;
    public TMP_Text counterText;

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } 
        else 
        {
            Debug.LogError("Instance already created");
            return;
        }
    }

    public void SetValue(int value)
    {
        counterText.text = value.ToString() + "/" + CardManager.Instance.cards.Count;
    }
}
