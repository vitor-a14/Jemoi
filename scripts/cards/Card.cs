using System.Collections.Generic;
using Godot;

public partial class Card : Control
{
	[Export] public Emoji emojiResource;

	private TextureRect artwork;

	protected void SetCard()
	{
		GetNode<TextureButton>("Button").Modulate = ResourcesManager.Instance.rarityColors[emojiResource.rarity];
		artwork = GetNode<TextureRect>("Artwork");
		artwork.Texture = emojiResource.artwork;
	}

	public virtual void OnButtonDown()
	{
		if(artwork == null) return;
		artwork.Position += new Vector2(0, 3);
	}

	public virtual void OnButtonUp()
	{
		if(artwork == null) return;
		artwork.Position -= new Vector2(0, 3);
	}
}
