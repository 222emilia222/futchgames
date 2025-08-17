using Godot;
using System;

public partial class interactible : Area2D
{
	[Export] CanvasItem mainSprite;
	[Export] int radiusKey; //1 is small, 2 is medium, 3 is nose, 0 is default
	[Export] int itemKey; //1=white,2=red,3=blue,4=yellow,5=lipstick,6=eyeliner,7=nose.
	bool hovered = false;
	Node2D grandparent;

	public override void _Ready()
	{
		mainSprite.Visible = true;
		grandparent = GetNode<Node2D>("../..");
    }

	public override void _Process(double delta)
	{
		if (hovered && Input.IsMouseButtonPressed(MouseButton.Left))
		{
            mainSprite.Visible = false;
			CollisionShape2D shape = GetNode<CollisionShape2D>("Coll2d");
            shape.Disabled = true;
			GetNode<itemmanager>("..").SwitchItem(mainSprite, shape, itemKey);
            grandparent.Call("_set_cursor", radiusKey);
        }
	}

	void On_mouse_entered()
	{
		hovered = true;
		grandparent.Call("_on_interactible_mouse_entered");
	}

	void On_mouse_exited()
	{
		hovered = false;
        grandparent.Call("_on_interactible_mouse_exited");
    }
}
