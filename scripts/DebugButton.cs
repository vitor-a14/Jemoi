using Godot;

public partial class DebugButton : Node
{
	[Export] public PackedScene card;
	[Export] public SmoothContainer container;

	public void OnButtonDown() 
	{
		var cardNode = card.Instantiate() as Control;
		GetTree().CurrentScene.AddChild(cardNode);
		container.AddContent(cardNode);
	}
}
