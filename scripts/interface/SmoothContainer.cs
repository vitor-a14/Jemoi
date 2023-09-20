using System.Collections.Generic;
using Godot;

public struct ContainerContent 
{
	public Control content;
	public Control pivot;

	public ContainerContent(Control content, Control pivot)
	{
		this.content = content;
		this.pivot = pivot;
	}
}

public partial class SmoothContainer : Node
{
	[Export] public PackedScene pivotScene;
	[Export] public float contentMovementSpeed;

	public List<ContainerContent> contents = new List<ContainerContent>();

	public override void _PhysicsProcess(double delta) 
	{
		foreach(ContainerContent containerContent in contents) 
		{
			float speed = (float)delta * contentMovementSpeed;
			containerContent.content.Position = containerContent.content.Position.Lerp(containerContent.pivot.GlobalPosition, speed);
		}
	}

	public void AddContent(Control content) 
	{
		Control pivotNode = pivotScene.Instantiate() as Control;
		AddChild(pivotNode);
		contents.Add(new ContainerContent(content, pivotNode));
	}

	public void RemoveContent(Control node)
	{
		foreach(ContainerContent containerContent in contents) 
		{
			if(containerContent.content == node) 
			{
				containerContent.pivot.QueueFree();
				containerContent.content.QueueFree();
				contents.Remove(containerContent);
				break;
			}
		}
	}
}
