using System.Collections;
using System.Threading.Tasks;
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
        StartCoroutine(StartButtonCoroutine());
    }

    public void ShopButton()
    {
        StartCoroutine(ShopButtonCoroutine());
    }

    public void RemoveAdsButton()
    {
        StartCoroutine(RemoveAdsButtonCoroutine());
    }

    private IEnumerator StartButtonCoroutine()
    {
        Fade.Instance.PlayFadeOut();

        yield return new WaitForSeconds(Fade.Instance.duration);

        CardManager.Instance.UpdatePlayerDeck();
        SceneManager.LoadScene("Gameplay");
    }

    private IEnumerator ShopButtonCoroutine()
    {
        Fade.Instance.PlayFadeOut();

        yield return new WaitForSeconds(Fade.Instance.duration);

        SceneManager.LoadScene("Shop");
    }

    private IEnumerator RemoveAdsButtonCoroutine()
    {
        Fade.Instance.PlayFadeOut();

        yield return new WaitForSeconds(Fade.Instance.duration);
    }
}
