using Godot;
using System;

public partial class Audiomanager : Control
{
    public static Audiomanager Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
    }

    public void StartMusic()
    {

    }
    public void SwitchToCreditsMusic()
    {

    }

    public void PlaySound_Powder()
    {

    }
    public void PlaySound_NosePlace()
    {

    }
    public void PlaySound_PaperRustle()
    {

    }
    public void PlaySound_NoseFall()
    {

    }
}