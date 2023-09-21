using System;
using System.Threading.Tasks;
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
	[Export] public int playerDrawAmount;

	private RandomNumberGenerator randomIndex = new RandomNumberGenerator();
	private PlayerCard selectedPlayerCard;
	private Timer timer;

	private Control movingCard;
	private Control targetCard;
	private float cardMovementTimer = 0f;
	private bool movingCards = false;

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
	
	private async void DelayedReady()
	{
		await ToSignal(GetTree().CreateTimer(0.1), "timeout");

		AddCard(playerCardInstance, 3, playerContainer, new Vector2(0, 0));
		AddCard(enemyCardInstance, 3, enemyContainer, new Vector2(0, 0));
	}

	public void SelectPlayerCard(PlayerCard playerCard) 
	{
		if(movingCards) return;
		if (selectedPlayerCard != null) selectedPlayerCard.Scale = new Vector2(1f, 1f);
		selectedPlayerCard = playerCard;
		selectedPlayerCard.Scale = new Vector2(1.2f, 1.2f);
	}

	private void AddCard(PackedScene cardInstance, int quantity, SmoothContainer container, Vector2 instancePosition)
	{
		for(int i = 0; i < quantity; i++)
		{
			var cardNode = cardInstance.Instantiate() as Control;
			GetTree().CurrentScene.AddChild(cardNode);
			cardNode.Position = instancePosition;
			container.AddContent(cardNode);
		}
	}

	private void RemoveCard(Control card, SmoothContainer container)
	{
		container.RemoveContent(card);
	}

	public void AttackCard(EnemyCard enemyTargetCard)
	{
		if(selectedPlayerCard == null || movingCards) return;

		var emojiResourceName = selectedPlayerCard.emojiResource.name;
		cardMovementTimer = 0f;
		movingCards = true;

		if(enemyTargetCard.emojiResource.weaknesses.Contains(emojiResourceName) 
		|| (enemyTargetCard.emojiResource.name == emojiResourceName && randomIndex.RandiRange(0, 1) == 0))
		{
			playerContainer.RemoveContent(selectedPlayerCard);
			movingCard = selectedPlayerCard;
			targetCard = enemyTargetCard;
		} 
		else
		{
			enemyContainer.RemoveContent(enemyTargetCard);
			movingCard = enemyTargetCard;
			targetCard = selectedPlayerCard;
		}

		movingCard.ZIndex = 5;
	}

	public override void _PhysicsProcess(double delta) 
	{
		if(movingCard != null && targetCard != null)
		{
			if(cardMovementTimer < 0.65f)
			{
				cardMovementTimer += (float) delta;
				movingCard.Position = movingCard.Position.Lerp(targetCard.GlobalPosition, playerContainer.contentMovementSpeed * (float) delta);
			}
			else
			{
				playerContainer.RemoveContent(selectedPlayerCard);
				selectedPlayerCard.QueueFree();
				selectedPlayerCard = null;
				movingCards = false;

				if(movingCard.GetType() == typeof(EnemyCard))
				{
					enemyContainer.AddContent(movingCard);
					movingCard.ZIndex = 0;
					targetCard = null;
				}
				else
				{
					if(playerContainer.contents.Count < maxPlayerCards - 1)
						AddCard(playerCardInstance, playerDrawAmount, playerContainer, targetCard.GlobalPosition);
					else 
						AddCard(playerCardInstance, 1, playerContainer, targetCard.GlobalPosition);

					enemyContainer.RemoveContent(targetCard);
					targetCard.QueueFree();
					targetCard = null;
				}
			}
		}
	}

	private void TurnEnd()
	{
		if(enemyContainer.contents.Count < maxEnemyCards && !movingCards) 
		{
			AddCard(enemyCardInstance, 1, enemyContainer, new Vector2(0, 0));
		}
	}
}
