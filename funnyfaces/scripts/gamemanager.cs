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
    [Export]
    SubViewport subViewport;
    float initScale;
    [Export] float finalScale, transitionShrinkTime, waitTime, fallSpeed;
    itemmanager controller;


    public override void _Ready()
    {
        initScale = blackhole.Scale.X;
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
        blackhole.Scale = new Vector2(initScale, initScale);
        blackhole.Visible = false;
        GetNode<itemmanager>("Item Control").currentItem = 0;
        GetNode<itemmanager>("CursorManager/Item Control").nosePlaced = false;
        GetNode<Node2D>("CursorManager").Call("_toggle_cursor_vis", true); //doesnt work
        subViewport.RenderTargetClearMode = SubViewport.ClearMode.Once;
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
