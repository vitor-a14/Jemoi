using Godot;

public partial class ShopManager : Node
{
	[Export] public PackedScene shopCardInstance;
	[Export] public GridContainer container;

	public override void _Ready()
	{
		foreach(Emoji emoji in EmojiManager.Instance.emojis)
		{
			var cardNode = shopCardInstance.Instantiate() as ShopCard;
			container.AddChild(cardNode);

			cardNode.emojiResource = emoji;
			cardNode.GetNode<Label>("Price").Text = emoji.price.ToString();
			cardNode.SetCard();
		}
	}
}
