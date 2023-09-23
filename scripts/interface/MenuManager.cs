using Godot;

public partial class MenuManager : Node
{
	public void PlayButton()
	{
		GetTree().ChangeSceneToFile("res://scenes/Gameplay.tscn");
	}

	public void ShopButton()
	{
		GetTree().ChangeSceneToFile("res://scenes/Shop.tscn");
	}

	public void RemoveAdsButton()
	{

	}
}
