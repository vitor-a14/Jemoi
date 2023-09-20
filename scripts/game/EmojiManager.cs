using Godot;
using System.Collections.Generic;
using System.IO;

public partial class EmojiManager : Node
{
	public static EmojiManager Instance { get; private set; }

	public List<Emoji> emojis = new List<Emoji>();
	
	private string path = "res://resources/emojis/";
	private RandomNumberGenerator randomIndex = new RandomNumberGenerator();

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr("Instance of EmojiManager is already running");

		foreach (string file in Directory.GetFiles(ProjectSettings.GlobalizePath(path)))
		{
			Emoji emojiCustomRes = GD.Load(file) as Emoji;
			emojis.Add(emojiCustomRes);
		}
	}

	public Emoji GetRandomEmoji() 
	{
		return emojis[randomIndex.RandiRange(0, emojis.Count - 1)];
	}
}
