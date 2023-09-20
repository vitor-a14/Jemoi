public partial class EnemyCard : Card
{
	public override void _Ready()
	{
		emojiResource = EmojiManager.Instance.GetRandomEmoji();
		SetCard();
	}

	public override void OnButtonDown() 
	{
		base.OnButtonDown();
	}

	public override void OnButtonUp() 
	{
		base.OnButtonUp();
	}
}
