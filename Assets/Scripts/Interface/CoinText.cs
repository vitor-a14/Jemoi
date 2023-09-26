using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public static CoinText Instance;  
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text scoreText;

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

    public void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();
    }
}
