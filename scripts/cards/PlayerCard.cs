public partial class PlayerCard : Card
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
		MatchManager.Instance.SelectPlayerCard(this);
	}
}
