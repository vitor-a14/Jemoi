using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public static CoinText Instance;  
    public TMP_Text coinText;

    private void Awake()
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

    public void UpdateCoinText(int value)
    {
        coinText.text = value.ToString();
    }
}
