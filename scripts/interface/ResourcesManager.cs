using System.Threading.Tasks;
using Godot;
using Godot.Collections;

public partial class ResourcesManager : Node
{
	public static ResourcesManager Instance { get; private set; }

	[Export] public Label coinText;

	public int currentCoins;
	
	public Dictionary<Rarity, Color> rarityColors = new Dictionary<Rarity, Color>
	{
		{ Rarity.NORMAL, new Color(0, 0.5f, 0.5f, 1) },
		{ Rarity.UNCOMMON, new Color(0, 0, 0, 1) },
		{ Rarity.RARE, new Color(0, 0, 0, 1) },
		{ Rarity.EPIC, new Color(0, 0, 0, 1) }
	};

	private float targetY;
	private float defaultTargetY;
	private float movementY;

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr("Instance of ResourcesManager is already running");

		SetCoins(0);

		defaultTargetY = coinText.GlobalPosition.Y;
		targetY = defaultTargetY;
	}

	public void AddCoins(int value)
	{
		currentCoins += value;
		currentCoins = Mathf.Clamp(currentCoins, 0, 99999);

		coinText.Text = currentCoins.ToString();
		TriggerAnimation();
	}

	public void SetCoins(int value)
	{
		currentCoins = value;
		currentCoins = Mathf.Clamp(currentCoins, 0, 99999);

		coinText.Text = currentCoins.ToString();
		TriggerAnimation();
	}

	private async void TriggerAnimation()
	{
		targetY = -25;
		await Task.Delay(20);
		targetY = defaultTargetY;
	}

	public override void _PhysicsProcess(double delta) 
	{
		movementY = Mathf.Lerp(movementY, targetY, 10f * (float)delta);
		coinText.Position = new Vector2(coinText.Position.X, movementY);
	}

}
