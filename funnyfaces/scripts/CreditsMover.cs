using Godot;
using System;

public partial class CreditsMover : Node2D
{
	[Export] float scrollSpeed;
	public override void _Ready()
	{
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position:y", Position.Y - 3000, scrollSpeed);
	}

	public override void _Process(double delta)
	{
        if (Input.IsKeyPressed(Key.Escape))
		{
			GetTree().Quit();
		}
    }
}
