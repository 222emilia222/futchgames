using Godot;
using System;

public partial class CreditsMover : Node2D
{
	[Export] float scrollSpeed;
	float startPos;
	public override void _Ready()
	{
		startPos = Position.Y;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position:y", startPos - 3000, scrollSpeed);
	}

	public override void _Process(double delta)
	{
        if (Input.IsKeyPressed(Key.Escape) || Position.Y < startPos - 2900)
		{
			GetTree().Quit();
		}
    }
}
