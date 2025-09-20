using Godot;
using System;

public partial class Audiomanager : Control
{
    public static Audiomanager Instance { get; private set; }
    [Export] AudioStreamPlayer2D musicPlayer, sfxPlayer;

    public override void _Ready()
    {
        Instance = this;
    }

    public void StartMusic()
    {
        musicPlayer.Play();
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