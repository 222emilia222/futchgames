using Godot;
using System;

public partial class MousePosition : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	Node2D MouseCursor;

	public override void _Process(double delta)
	{
        MouseCursor.Position = GetViewport().GetMousePosition();
    }
}
