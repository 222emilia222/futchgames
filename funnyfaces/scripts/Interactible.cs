using Godot;
using System;

public partial class Interactible : Area2D
{
	[Export] CanvasItem whiteFrame;
	[Export] CanvasItem mainSprite;
	[Export] int radiusKey; //1 is small, 2 is medium, 3 is nose, 0 is default
	bool hovered = false;
	Node2D parent;

	public override void _Ready()
	{
		whiteFrame.Visible = false;
		mainSprite.Visible = true;
		parent = GetNode<Node2D>("..");
    }

	public override void _Process(double delta)
	{
		if (hovered && Input.IsMouseButtonPressed(MouseButton.Left))
		{
            mainSprite.Visible = false;
			GetNode<CollisionShape2D>("Coll2d").Disabled = true;
			//subscribe visible und enable to event on other makeup
			parent.Call("_set_cursor", radiusKey);
        }
	}

	void On_mouse_entered()
	{
		hovered = true;
		whiteFrame.Visible = true;
		parent.Call("_on_interactible_mouse_entered");
	}

	void On_mouse_exited()
	{
		hovered = false;
        whiteFrame.Visible = false;
        parent.Call("_on_interactible_mouse_exited");
    }
}
