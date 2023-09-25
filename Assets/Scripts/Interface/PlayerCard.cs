public class PlayerCard : Card
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
        GameplayManager.Instance.SelectPlayerCard(this);
    }
}
