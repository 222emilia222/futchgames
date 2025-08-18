using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class itemmanager : Control
{
    [Export] Sprite2D currentBrush;
    [Export] Texture2D whitePowder, colorPowder, wetBrush, wetBrushSmall;
    [Export] Color blue, yellow, red, wetRed;
    [Export] double spacingPowder, spacingWet;
    [Export] double currentTimer;
    int currentItem = 0;
    bool clicked = false;

    public override void _Ready()
    {
        currentBrush.Modulate = Color.Color8(255, 255, 255, 255);
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
            case 1:
                { currentTimer = spacingPowder; currentBrush.Texture = whitePowder; currentBrush.Modulate = Color.Color8(255, 255, 255, 255); break; }
            case 2:
                { currentTimer = spacingPowder; currentBrush.Texture = colorPowder; currentBrush.Modulate = red; break; }
            case 3:
                { currentTimer = spacingPowder; currentBrush.Texture = colorPowder; currentBrush.Modulate = blue; break; }
            case 4:
                { currentTimer = spacingPowder; currentBrush.Texture = colorPowder; currentBrush.Modulate = yellow; break; }
            case 5:
                { currentTimer = spacingWet; currentBrush.Texture = wetBrush; currentBrush.Modulate = wetRed; break; }
            case 6:
                { currentTimer = spacingWet; currentBrush.Texture = wetBrushSmall; currentBrush.Modulate = Color.Color8(0, 0, 0, 255); break; }
            case 7:
                { currentBrush.Texture = null; break; }
        }
    }
    #endregion

    #region Painting
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
            // nose placed!
            // cursor open hand
            // no clicky on items
            // transition start
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
        currentBrush.Visible = true; //enable brush
        await mainLoop.ToSignal(mainLoop, SceneTree.SignalName.ProcessFrame);
        currentBrush.Visible = false; //disable brush
        await ToSignal(GetTree().CreateTimer(currentTimer), Godot.Timer.SignalName.Timeout);
        Place(token);
    }
    #endregion
}
