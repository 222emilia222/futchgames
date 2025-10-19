using Godot;
using System;
using System.Threading.Tasks;

public partial class gamemanager : Node2D
{
    [ExportCategory("Order")]
    [Export] Godot.Collections.Array<Node2D> characters;
    [Export] Godot.Collections.Array<Node2D> smiles;
    [Export] float hintTime, smileDelay;
    [Export] RichTextLabel hint;
    int currentChar = 0;
    [ExportCategory("Transition")]
    [Export] Node2D blackhole;
    ViewportTexture paintboardTex;
    [Export] SubViewport subViewport;
    [Export] Node2D cursorManager;
    private float initScale;
    [Export] float finalScale, transitionShrinkTime, waitTime, fallSpeed;


    public override void _Ready()
    {
        initScale = blackhole.Scale.X;
        characters[currentChar].Visible = true;
        for (int i = 1; i < characters.Count; i++)
        {
            characters[i].Visible = false;
            smiles[i].Visible = false;
        }
        hint.Visible = false;
        TimedHint();
    }

    public void Resetup()
    {
        hint.Visible = false;
        if (currentChar == characters.Count - 1) { CueEndScene(); GD.Print("credits!!"); return; }
        characters[currentChar].Visible = false; smiles[currentChar].Visible = false; currentChar++;
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
        Audiomanager.Instance.PlaySound(0);
        if (currentChar == characters.Count - 1) { Audiomanager.Instance.SwitchToCreditsMusic(); }
        await ToSignal(GetTree().CreateTimer(smileDelay), Timer.SignalName.Timeout);
        smiles[currentChar].Visible = true;
        blackhole.Position = GetNode<itemmanager>("CursorManager/Item Control").nose.Position + new Vector2(0, 15);
        blackhole.Visible = true;
        Tween tween = GetTree().CreateTween().SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(blackhole, "scale", new Vector2(finalScale, finalScale), transitionShrinkTime);
        await ToSignal(GetTree().CreateTimer(transitionShrinkTime + waitTime), Timer.SignalName.Timeout);
        Tween tween2 = GetTree().CreateTween().SetEase(Tween.EaseType.InOut);
        Audiomanager.Instance.PlaySound(1);
        //nose rotation
        tween2.TweenProperty(blackhole, "position:y", blackhole.Position.Y + 1100, fallSpeed);
        Sprite2D nose = GetNode<itemmanager>("CursorManager/Item Control").nose;
        tween2.Parallel().TweenProperty(nose, "position:y", nose.Position.Y + 1100, fallSpeed);
        await ToSignal(GetTree().CreateTimer(waitTime + fallSpeed), Timer.SignalName.Timeout);

        Resetup();
    }

    private async Task TimedHint()
    {
        await ToSignal(GetTree().CreateTimer(hintTime), Timer.SignalName.Timeout);
        hint.Visible = true;
    }

    public void CueEndScene()
    {
        var scene =  GD.Load<PackedScene>("res://scenes/end.tscn");
        var inst = scene.Instantiate();
        AddChild(inst);
    }
}
