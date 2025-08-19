using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class itemmanager : Control
{
    [Export] public Sprite2D brush, nose;
    [Export] Node2D mousePos;
    [Export] Texture2D whitePowder, colorPowder, wetBrush, wetBrushSmall;
    [Export] Color blue, yellow, red, wetRed;
    [Export] double spacingPowder, spacingWet;
    [Export] double currentTimer;
    public int currentItem = 0;
    bool clicked = false;
    public bool nosePlaced = false;

    public override void _Ready()
    {
        nose.Visible = false;
        brush.Modulate = Color.Color8(255, 255, 255, 255);
    }

    #region Item Switching
    CanvasItem sprite;
    CollisionShape2D coll;
    bool isLoaded = false;
    bool done = false;

    public void SwitchItem(CanvasItem newSprite, CollisionShape2D newColl, int key)
    {
        currentItem = key;
        if (isLoaded && newSprite != sprite)
        {
            sprite.Visible = true;
            coll.Disabled = false;
        }
        sprite = newSprite;
        coll = newColl;
        isLoaded = true;

        //change brush sprite & timer
        switch (currentItem)
        {
            case 0:
                { brush.Texture = null; break; }
            case 1:
                { currentTimer = spacingPowder; brush.Texture = whitePowder; brush.Modulate = Color.Color8(255, 255, 255, 255); break; }
            case 2:
                { currentTimer = spacingPowder; brush.Texture = colorPowder; brush.Modulate = red; break; }
            case 3:
                { currentTimer = spacingPowder; brush.Texture = colorPowder; brush.Modulate = blue; break; }
            case 4:
                { currentTimer = spacingPowder; brush.Texture = colorPowder; brush.Modulate = yellow; break; }
            case 5:
                { currentTimer = spacingWet; brush.Texture = wetBrush; brush.Modulate = wetRed; break; }
            case 6:
                { currentTimer = spacingWet; brush.Texture = wetBrushSmall; brush.Modulate = Color.Color8(0, 0, 0, 255); break; }
            case 7:
                { brush.Texture = null; break; }
        }
    }
    #endregion

    #region Painting & Nose Placing
    public override void _Process(double delta)
    {
        if (!Input.IsMouseButtonPressed(MouseButton.Left)) { clicked = false; }
        if (Input.IsMouseButtonPressed(MouseButton.Left) && !clicked) { OnClick(); }
    }

    CancellationTokenSource _token;

    void OnClick()
    {
        clicked = true;
        if (currentItem == 7)
        {
            if (!nosePlaced)
            {
                PlaceNose();
                GD.Print("nose placed! I repeat! nose placed!");
            }
            return;
        }
        if (_token != null)
        {
            _token.Cancel();
        }
        _token = new CancellationTokenSource();
        Place(_token.Token);
    }

    public async Task Place(CancellationToken token)
    {
        if (token.IsCancellationRequested || !Input.IsMouseButtonPressed(MouseButton.Left))
        {
            return;
        }

        var mainLoop = Engine.GetMainLoop();
        brush.Rotation += 30f; // move brush rotation
        brush.Visible = true; //enable brush
        await mainLoop.ToSignal(mainLoop, SceneTree.SignalName.ProcessFrame);
        brush.Visible = false; //disable brush
        await ToSignal(GetTree().CreateTimer(currentTimer), Godot.Timer.SignalName.Timeout);
        Place(token);
    }

    void PlaceNose()
    {
        nosePlaced = true;
        nose.Position = mousePos.Position + new Vector2(-24, -8);
        nose.Visible = true;
        GetNode<Node2D>("..").Call("_set_cursor_vis", false);
        GetNode<gamemanager>("../..").TransitionStart();
    }
    #endregion
}
