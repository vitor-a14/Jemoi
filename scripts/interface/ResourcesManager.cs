using Godot;

public partial class ResourcesManager : Node
{
	public static ResourcesManager Instance { get; private set; }

	[Export] public Label coinText;

	public int currentCoins;

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr("Instance of ResourcesManager is already running");

		SetCoins(0);
	}

	public void AddCoins(int value)
	{
		currentCoins += value;
		currentCoins = Mathf.Clamp(currentCoins, 0, 99999);

		coinText.Text = currentCoins.ToString();
	}

	public void SetCoins(int value)
	{
		currentCoins = value;
		currentCoins = Mathf.Clamp(currentCoins, 0, 99999);

		coinText.Text = currentCoins.ToString();
	}

}
