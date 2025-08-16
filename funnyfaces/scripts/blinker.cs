using Godot;
using System;

public partial class blinker : Sprite2D
{

	[Export] Texture2D eyes1, eyes2;
	float timer;
	float blinkTimer;
	bool timer2 = false;
	Random r = new Random();

	public override void _Ready()
	{
		timer = 4.5f;
	}

	public override void _Process(double delta)
	{
		if (timer2) { blinkTimer -= Convert.ToSingle(delta); }
		else { timer -= Convert.ToSingle(delta); }

		if (timer <= 0 || blinkTimer <= 0)
		{
			if (timer2) { Texture = eyes1; }
			else { Texture = eyes2; }
			blinkTimer = r.Next(4, 16) / 10f;
            timer = r.Next(25, 65) / 10f;
            timer2 = !timer2;
		}
	}
}
