using Godot;
using System;

public partial class itemmanager : Control
{
    [Export] float spacingPowder, spacingWet;
    float currentTimer = 0;
    int currentItem = 0;

    #region Item Switching
    CanvasItem sprite;
    CollisionShape2D coll;
    bool isLoaded = false;

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
    }
    #endregion

    [Export] Sprite2D brush;

    public override void _Process(double delta)
    {
        currentTimer -= Convert.ToSingle(delta);

        if (Input.IsMouseButtonPressed(MouseButton.Left) && currentTimer <= 0)
        {
            switch (currentItem)
            {
                case 1:
                    {

                        currentTimer = spacingPowder;
                        break;
                    }
            }
        }
        else { currentTimer = 0; }
    }
}
