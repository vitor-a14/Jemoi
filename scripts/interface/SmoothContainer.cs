using System.Collections.Generic;
using Godot;

public class ContainerContent 
{
	public Control content;
	public Control pivot;
	public bool isActive;

	public ContainerContent(Control content, Control pivot, bool isActive)
	{
		this.content = content;
		this.pivot = pivot;
		this.isActive = isActive;
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
			if(containerContent.isActive) 
			{
				containerContent.content.Position = containerContent.content.Position.Lerp(containerContent.pivot.GlobalPosition, (float)delta * contentMovementSpeed);
			}
		}
	}

	public void AddContent(Control content) 
	{
		Control pivotNode = pivotScene.Instantiate() as Control;
		AddChild(pivotNode);
		contents.Add(new ContainerContent(content, pivotNode, true));
	}

	public void RemoveContent(Control node)
	{
		foreach(ContainerContent containerContent in contents) 
		{
			if(containerContent.content == node) 
			{
				containerContent.pivot.QueueFree();
				contents.Remove(containerContent);
				break;
			}
		}
	}
}
