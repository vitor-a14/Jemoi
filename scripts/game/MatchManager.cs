using Godot;

public partial class MatchManager : Node
{
	public static MatchManager Instance { get; private set; }

	[Export] public SmoothContainer playerContainer;
	[Export] public SmoothContainer enemyContainer;

	[Export] public PackedScene playerCardInstance;
	[Export] public PackedScene enemyCardInstance;

	[Export] public int maxEnemyCards;
	[Export] public int maxPlayerCards;

	private PlayerCard selectedPlayerCard;
	private Timer timer;

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr("Instance of EmojiManager is already running");

		timer = GetNode<Timer>("Timer");
		timer.Timeout += () => TurnEnd();

		DelayedReady();
	}
	
	private void TurnEnd()
	{
		if(enemyContainer.contents.Count < maxEnemyCards) 
		{
			AddCard(enemyCardInstance, 1, enemyContainer);
		}
	}

	private async void DelayedReady()
	{
		await ToSignal(GetTree().CreateTimer(0.1), "timeout");

		AddCard(playerCardInstance, 3, playerContainer);
		AddCard(enemyCardInstance, 4, enemyContainer);
	}

	public void SelectPlayerCard(PlayerCard playerCard) 
	{
		if (selectedPlayerCard != null) selectedPlayerCard.Scale = new Vector2(1f, 1f);

		selectedPlayerCard = playerCard;

		selectedPlayerCard.Scale = new Vector2(1.2f, 1.2f);
	}

	public override void _Process(double delta)
	{

	}

	private void AddCard(PackedScene cardInstance, int quantity, SmoothContainer container)
	{
		for(int i = 0; i < quantity; i++)
		{
			var cardNode = cardInstance.Instantiate() as Control;
			GetTree().CurrentScene.AddChild(cardNode);
			container.AddContent(cardNode);
		}
	}

	private void RemoveCard(Control card, SmoothContainer container)
	{
		container.RemoveContent(card);
	}
}
