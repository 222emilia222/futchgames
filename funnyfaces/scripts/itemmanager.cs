using Godot;
using System;

public partial class itemmanager : Control
{
    CanvasItem sprite;
    CollisionShape2D coll;
    bool isLoaded = false;

    public void SwitchItem(CanvasItem newSprite, CollisionShape2D newColl)
    {
        if (isLoaded && newSprite != sprite)
        {
            sprite.Visible = true;
            coll.Disabled = false;
        }
        sprite = newSprite;
        coll = newColl;
        isLoaded = true;
    }
}
