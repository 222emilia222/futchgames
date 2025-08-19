using Godot;
using System;

public partial class Hint : RichTextLabel
{
	[Export] double timeUntilHint;
	public override void _Process(double delta)
	{
		timeUntilHint -= delta;
		if (timeUntilHint <= 0)
		{
			Tween tween = GetTree().CreateTween();
			tween.TweenProperty(this, "modulate", new Color(255, 255, 255, 255), 1.2);
		}
	}
    public override void _Ready()
	{
		this.Modulate = new Color(255, 255, 255, 0);
	}
}
