using Godot;
using System;

public partial class breather : Sprite2D
{
    [Export]
    float breatheInterval;
    [Export]
    float movValue;

    public override void _Ready()
    {
        Tween tween = GetTree().CreateTween().SetLoops().SetEase(Tween.EaseType.OutIn);
        tween.TweenProperty(this, "position:y", -movValue, breatheInterval);
        tween.TweenProperty(this, "position:y", 0, breatheInterval);
    }
}
