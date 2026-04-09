using Godot;
using System;

public partial class CreditsMover : Node2D
{
	[Export] float scrollSpeed;
	private float startPos;
	[Export] Godot.Collections.Array<Sprite2D> portraitSprites;
	public override void _Ready()
	{
		for (int i = 0; i < portraitSprites.Count; i++)
		{
			var img = Image.LoadFromFile("user://char_portrait_" + i + ".png");
            portraitSprites[i].Texture = ImageTexture.CreateFromImage(img);
        }
		startPos = Position.Y;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position:y", startPos - 3000, scrollSpeed);
	}

	public override void _Process(double delta)
	{
        if (Input.IsKeyPressed(Key.Escape) || Position.Y <= startPos - 3000)
		{
			GetTree().Quit();
		}
    }
}
