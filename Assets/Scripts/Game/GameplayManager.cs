using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Difficulty
{
    public int untilScore;
    public Rarity maxCardRarity;
    public float enemyCardDelay;
}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;  

    [HideInInspector] public PlayerCard selectedPlayerCard; 

    [Header("General")]
    public int maxPlayerCards, maxEnemyCards;
    public int coinsPerWin;

    [Header("Player And Enemy Cards")]
    public int attackDuration;
    public GameObject enemyCardInstance, playerCardInstance;
    public Transform screen, enemyContainer, playerContainer;

    [Header("Game Over")]
    public GameObject gameOverScreen;
    public TMP_Text gameOverScore, gameOverCoins;

    [Header("Difficulty Management")]
    public float enemyCardSpawnDelay;
    public Rarity maxEnemyCardRarity;
    public List<Difficulty> difficulties = new List<Difficulty>();

    private bool inAnimation = false;
    private Transform attacker, target;
    private float enemyCardSpawnTimer = 0f;

    private int currentCoins = 0;
    private int currentScore = 0;
    private bool gameOver = false;

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

        screen = GameObject.FindGameObjectWithTag("Screen").transform;
        CoinText.Instance.UpdateCoinText(currentCoins);
        CoinText.Instance.UpdateScoreText(currentScore);
        UpdateDifficulty();
    }

    public void SelectPlayerCard(PlayerCard playerCard)
    {
        if(inAnimation || gameOver) return;

        if (selectedPlayerCard != null)
            selectedPlayerCard.transform.localScale = new Vector3(1, 1, 1);

        selectedPlayerCard = playerCard;

        selectedPlayerCard.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public async void AttackEnemyCard(EnemyCard enemyCard)
    {
        if (selectedPlayerCard == null || inAnimation || gameOver) return;

        inAnimation = true;
        bool playerWon = CardManager.Instance.CheckWeakness(selectedPlayerCard, enemyCard);

        attacker = playerWon ? selectedPlayerCard.transform : enemyCard.transform;
        target = playerWon ? enemyCard.transform : selectedPlayerCard.transform;
        attacker.SetParent(screen);

        await Task.Delay(attackDuration); // wait 0.6 seconds

        Destroy(selectedPlayerCard.gameObject);
        if(playerWon)
        {
            AddPlayerCard(2);
            currentCoins += coinsPerWin;
            currentScore += 1;
            CoinText.Instance.UpdateCoinText(currentCoins);
            CoinText.Instance.UpdateScoreText(currentScore);
            UpdateDifficulty();
            Destroy(enemyCard.gameObject);
        } 
        else 
        {
            enemyCard.transform.SetParent(enemyContainer.transform);
        }

        inAnimation = false;
    }

    private void Update()
    {
        if(gameOver) return;

        if(playerContainer.childCount <= 0 && selectedPlayerCard == null) 
            GameOver();

        if(inAnimation)
            attacker.position = Vector3.Lerp(attacker.position, target.position, 8 * Time.deltaTime);
        else
            enemyCardSpawnTimer += Time.deltaTime;

        if(enemyCardSpawnTimer >= enemyCardSpawnDelay)
        {
            AddEnemyCard();
            enemyCardSpawnTimer = 0f;
        } 
    }

    private void AddPlayerCard(int quantity)
    {
        for(int i = 0; i < quantity; i++)
        {
            if(playerContainer.childCount >= maxPlayerCards) return;
            Transform playerCard = Instantiate(playerCardInstance, playerContainer).transform;
            playerCard.position = target.position;
        }
    }

    private void AddEnemyCard()
    {
        if(enemyContainer.childCount >= maxEnemyCards) 
        {
            GameOver();
            return;
        }

        Instantiate(enemyCardInstance, enemyContainer);
    }

    public void RetryButton()
    {
        StartCoroutine(RetryButtonCoroutine());
    }

    public void MenuButton()
    {
        StartCoroutine(MenuButtonCoroutine());
    }

    private void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        gameOverScore.text = currentScore.ToString();
        gameOverCoins.text = currentCoins.ToString();
    }

    private void UpdateDifficulty()
    {
        enemyCardSpawnDelay = difficulties[difficulties.Count - 1].enemyCardDelay;
        maxEnemyCardRarity = difficulties[difficulties.Count - 1].maxCardRarity;

        foreach(Difficulty difficulty in difficulties)
        {
            if(currentScore <= difficulty.untilScore)
            {
                enemyCardSpawnDelay = difficulty.enemyCardDelay;
                maxEnemyCardRarity = difficulty.maxCardRarity;
                break;
            }
        }
    }
    
    private IEnumerator RetryButtonCoroutine()
    {
        Fade.Instance.PlayFadeOut();

        yield return new WaitForSeconds(Fade.Instance.duration);

        ResourceManager.Instance.AddCoins(currentCoins);
        ResourceManager.Instance.SetMaxScore(currentScore);
        ResourceManager.Instance.Save();
        SceneManager.LoadScene("Gameplay");
    }

    private IEnumerator MenuButtonCoroutine()
    {
        Fade.Instance.PlayFadeOut();

        yield return new WaitForSeconds(Fade.Instance.duration);

        ResourceManager.Instance.AddCoins(currentCoins);
        ResourceManager.Instance.SetMaxScore(currentScore);
        ResourceManager.Instance.Save();
        SceneManager.LoadScene("Main Menu");
    }
}
