using Godot;
using System;

public partial class mouseMover : Node2D
{
	public override void _Process(double delta)
	{
		Position = GetViewport().GetMousePosition() + new Vector2(57, 35); //I dont like doing it this way, also needs to be tested if its a scaling issue
    }
}
