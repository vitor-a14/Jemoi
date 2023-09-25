using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
