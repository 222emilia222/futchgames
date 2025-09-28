using Godot;
using System;

public partial class Intro : Node2D
{
    private int clickCount = 0;
    [Export] private int interactionCount;

    public override void _Process(double delta)
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            if(clickCount == interactionCount) { SwitchToGameScene(); } //change this after timer etc
            clickCount++;
        }
    }

    void SwitchToGameScene()
    {
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }
}
