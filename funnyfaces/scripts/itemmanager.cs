using Godot;
using System;

public partial class itemmanager : Control
{
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
        switch (currentItem)
        {

        }
    }
}
