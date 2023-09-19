using Godot;

public partial class Card : Node
{
	[Export] public Emoji emojiResource;

	private TextureRect artwork;

	protected void SetCard()
	{
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
