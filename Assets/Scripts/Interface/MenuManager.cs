using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Start() {
        string scoreToString = ResourceManager.Instance.maxScore.ToString();

        if(scoreToString == "0" || scoreToString == null || scoreToString == "")
            scoreText.text = "";
        else
            scoreText.text = "MAX SCORE: " + scoreToString;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShopButton()
    {
        SceneManager.LoadScene("Shop");
    }

    public void RemoveAdsButton()
    {
        
    }
}
