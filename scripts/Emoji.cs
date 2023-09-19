using Godot;

[GlobalClass]
public partial class Emoji : Resource
{
    [Export] public int id { get; set; }
    [Export] public string name { get; set; }
    [Export] public int price { get; set; }
    [Export] public Texture2D artwork { get; set; }
    [Export] public string weaknesses { get; set; }

    public Emoji() 
    {
        id = -1;
        name = null;
        price = -1;
        artwork = null;
        weaknesses = null;
    }
}
