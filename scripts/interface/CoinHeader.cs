using System.Threading.Tasks;
using Godot;

public partial class CoinHeader : Node
{
	public static CoinHeader Instance { get; private set; }

	[Export] public Label coinText;

	private float targetY;
	private float defaultTargetY;
	private float movementY;

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr("Instance of CoinHeader is already running");

		defaultTargetY = coinText.GlobalPosition.Y;
		targetY = defaultTargetY;
	}

	public void ChangeCoinValue(int value)
	{
		coinText.Text = value.ToString();
		TriggerAnimation();
	}

	private async void TriggerAnimation()
	{
		targetY = -25;
		await ToSignal(GetTree().CreateTimer(0.02f), "timeout");
		targetY = defaultTargetY;
	}

	public override void _PhysicsProcess(double delta) 
	{
		movementY = Mathf.Lerp(movementY, targetY, 10f * (float)delta);
		coinText.Position = new Vector2(coinText.Position.X, movementY);
	}
}
