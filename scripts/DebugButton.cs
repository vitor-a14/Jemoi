using Godot;

public partial class DebugButton : Node
{

	[Export] public PackedScene card;

	public void OnButtonDown() 
	{
		var cardNode = card.Instantiate() as Control;
		GetTree().CurrentScene.AddChild(cardNode);
		SmoothContainer.Instance.AddContent(cardNode);
	}

}
