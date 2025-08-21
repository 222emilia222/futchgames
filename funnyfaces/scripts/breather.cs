using Godot;
using System;

public partial class breather : Node2D
{
    [Export]
    float breatheInterval;
    [Export]
    float movValue;
    float initPos;

    public override void _Ready()
    {
        initPos = this.Position.Y;
        Tween tween = GetTree().CreateTween().SetLoops().SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(this, "position:y", initPos - movValue, breatheInterval);
        tween.TweenProperty(this, "position:y", initPos, breatheInterval);
    }
}
