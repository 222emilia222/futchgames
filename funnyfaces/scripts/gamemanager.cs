using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class gamemanager : Node2D
{
    [ExportCategory("Order")]
    [Export] Godot.Collections.Array<Node2D> characters;
    int currentChar = 0;
    [ExportCategory("Transition")]
    [Export] Node2D blackhole;
    ViewportTexture paintboardTex;
    float initScale;
    [Export] float finalScale, transitionShrinkTime, waitTime, fallSpeed;
    itemmanager controller;

    public override void _Ready()
    {
        controller = GetNode<itemmanager>("CursorManager/Item Control");
        for (int i = 1; i < characters.Count; i++)
        {
            characters[i].Visible = false;
        }
        characters[currentChar].Visible = true;
    }

    public void Resetup()
    {
        characters[currentChar].Visible = false; currentChar++;
        characters[currentChar].Visible = true;
        initScale= blackhole.Scale.X;
        blackhole.Visible = false;
        GetNode<itemmanager>("CursorManager/Item Control").nosePlaced = false;
        GetNode<Node2D>("CursorManager").Call("_toggle_cursor_vis", true);
        //reset viewport texture
    }

    public async Task TransitionStart()
    {
        blackhole.Position = GetNode<itemmanager>("CursorManager/Item Control").nose.Position + new Vector2(0, 15);
        blackhole.Visible = true;
        Tween tween = GetTree().CreateTween().SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(blackhole, "scale", new Vector2(finalScale, finalScale), transitionShrinkTime);
        await ToSignal(GetTree().CreateTimer(transitionShrinkTime + waitTime), Godot.Timer.SignalName.Timeout);
        Tween tween2 = GetTree().CreateTween().SetEase(Tween.EaseType.InOut);
        tween2.TweenProperty(blackhole, "position:y", blackhole.Position.Y + 1100, fallSpeed);
        tween2.Parallel().TweenProperty(controller.nose, "position:y", controller.nose.Position.Y + 1100, fallSpeed);
        await ToSignal(GetTree().CreateTimer(waitTime + fallSpeed), Godot.Timer.SignalName.Timeout);

        Resetup();
    }
}
