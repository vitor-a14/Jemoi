using Godot;
using Godot.Collections;

public partial class ResourcesManager : Node
{
	public static ResourcesManager Instance { get; private set; }

	public int currentCoins;
	
	public Dictionary<Rarity, Color> rarityColors = new Dictionary<Rarity, Color>
	{
		{ Rarity.NORMAL, new Color(1f, 1f, 1f, 1f) },
		{ Rarity.UNCOMMON, new Color(0.12f, 0.66f, 1f, 1f) },
		{ Rarity.RARE, new Color(1f, 0.70f, 0f, 1f) },
		{ Rarity.EPIC, new Color(0.5f, 0f, 1f, 1f) }
	};

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr("Instance of ResourcesManager is already running");

		DelayedReady();
	}

	private async void DelayedReady()
	{
		await ToSignal(GetTree().CreateTimer(0.1), "timeout");

		SetCoins(0);
	}

	public void AddCoins(int value)
	{
		currentCoins += value;
		currentCoins = Mathf.Clamp(currentCoins, 0, 99999);

		if(CoinHeader.Instance != null) CoinHeader.Instance.ChangeCoinValue(currentCoins);
	}

	public void SetCoins(int value)
	{
		currentCoins = value;
		currentCoins = Mathf.Clamp(currentCoins, 0, 99999);

		if(CoinHeader.Instance != null) CoinHeader.Instance.ChangeCoinValue(currentCoins);
	}
}
