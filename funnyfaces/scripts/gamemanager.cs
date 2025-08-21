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
    [Export]
    Node2D cursorManager;
    float initScale;
    [Export] float finalScale, transitionShrinkTime, waitTime, fallSpeed;


    public override void _Ready()
    {
        initScale = blackhole.Scale.X;
        characters[currentChar].Visible = true;
        for (int i = 1; i < characters.Count; i++)
        {
            characters[i].Visible = false;
        }
    }

    public void Resetup()
    {
        if (currentChar == characters.Count - 1) { CueEndScene(); GD.Print("credits!!"); return; }
        characters[currentChar].Visible = false; currentChar++;
        characters[currentChar].Visible = true;
        blackhole.Scale = new Vector2(initScale, initScale);
        blackhole.Visible = false;
        GetNode<itemmanager>("CursorManager/Item Control").currentItem = 0;
        GetNode<itemmanager>("CursorManager/Item Control").nosePlaced = false;
        cursorManager.Call("_set_cursor_vis", true);
        cursorManager.Call("_set_cursor", 0);
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
        Sprite2D nose = GetNode<itemmanager>("CursorManager/Item Control").nose;
        tween2.Parallel().TweenProperty(nose, "position:y", nose.Position.Y + 1100, fallSpeed);
        await ToSignal(GetTree().CreateTimer(waitTime + fallSpeed), Godot.Timer.SignalName.Timeout);

        Resetup();
    }

    public void CueEndScene()
    {
        GetTree().ChangeSceneToFile("res://scenes/end.tscn");
    }
}
