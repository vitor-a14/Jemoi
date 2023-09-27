public class EnemyCard : Card
{
    void Start()
    {
        SetCard(CardManager.Instance.GetEnemyCard());
    }

    protected override void OnButtonDown()
    {
        base.OnButtonDown();
    }

    protected override void OnButtonUp()
    {
        GameplayManager.Instance.AttackEnemyCard(this);
    }
}
