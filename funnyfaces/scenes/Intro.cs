using Godot;
using System;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public partial class Intro : Node2D
{
    private int clickCount = 0;
    [Export] private Node2D contract, signature;
    [Export] private Vector3 pos1, pos2, posFin;
    [Export] private float startDelay, endDelay, transDelay;
    private bool isInStartPos;

    public override void _Ready()
    {
        signature.Visible = false;
        Start();
    }

    public override void _Process(double delta)
    {
        //if (Input.IsMouseButtonPressed(MouseButton.Left))
        //{
        //    if (clickCount == 1) 
        //    {
        //        Tween tween = GetTree().CreateTween().SetEase(Tween.EaseType.In);
        //        tween.TweenProperty(contract, "position", new Vector2(pos2.X, pos2.Y), pos2.Z);
        //    }
        //    else if (clickCount == interactionCount) { SwitchToGameScene(); }
        //    clickCount++;
        //}
    }

    public override void _Input(InputEvent @event)
    {
        if (!@event.IsEcho() && Input.IsMouseButtonPressed(MouseButton.Left))
        {
            GD.Print("clicked!");
            if (clickCount == 0 && isInStartPos)
            {
                Tween tween = GetTree().CreateTween().SetEase(Tween.EaseType.InOut);
                tween.TweenProperty(contract, "position", new Vector2(pos2.X, pos2.Y), pos2.Z);
                clickCount++;
            }
            else if (clickCount == 1) { SwitchToGameScene(); }
        }
    }

    private async Task Start()
    {
        await ToSignal(GetTree().CreateTimer(startDelay), Timer.SignalName.Timeout);
        Tween tween = GetTree().CreateTween().SetEase(Tween.EaseType.Out);
        tween.TweenProperty(contract, "position", new Vector2(pos1.X, pos1.Y), pos1.Z);
        await ToSignal(GetTree().CreateTimer(pos1.Z), Timer.SignalName.Timeout);
        isInStartPos = true;
    }

    private async Task SwitchToGameScene()
    {
        signature.Visible = true;
        await ToSignal(GetTree().CreateTimer(endDelay + posFin.Z), Timer.SignalName.Timeout);
        Tween tween = GetTree().CreateTween().SetEase(Tween.EaseType.In);
        tween.TweenProperty(contract, "position", new Vector2(posFin.X, posFin.Y), posFin.Z);
        await ToSignal(GetTree().CreateTimer(transDelay + posFin.Z), Timer.SignalName.Timeout);
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }
}
