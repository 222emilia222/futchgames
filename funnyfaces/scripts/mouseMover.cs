using Godot;
using System;

public partial class mouseMover : Node2D
{
	public override void _Process(double delta)
	{
		Position = GetViewport().GetMousePosition();
    }
}
