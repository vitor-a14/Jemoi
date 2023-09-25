using System.Threading.Tasks;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;  

    [HideInInspector] public PlayerCard selectedPlayerCard; 

    public int maxPlayerCards, maxEnemyCards;
    public int coinsPerWin;

    public int attackDuration;
    public float enemyCardSpawnDelay;
    public GameObject enemyCardInstance, playerCardInstance;
    public Transform screen, enemyContainer, playerContainer;
 
    private bool inAnimation = false;
    private Transform attacker, target;
    private float enemyCardSpawnTimer = 0f;

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
    }

    public void SelectPlayerCard(PlayerCard playerCard)
    {
        if(inAnimation) return;

        if (selectedPlayerCard != null)
            selectedPlayerCard.transform.localScale = new Vector3(1, 1, 1);

        selectedPlayerCard = playerCard;

        selectedPlayerCard.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public async void AttackEnemyCard(EnemyCard enemyCard)
    {
        if (selectedPlayerCard == null || inAnimation) return;

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
            ResourceManager.Instance.AddCoins(coinsPerWin);
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
        if(inAnimation)
        {
            attacker.position = Vector3.Lerp(attacker.position, target.position, 8 * Time.deltaTime);
        }

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
        if(enemyContainer.childCount >= maxEnemyCards) return;
        Instantiate(enemyCardInstance, enemyContainer);
    }
}
