using Godot;
using System;

public partial class Audiomanager : Control
{
    public static Audiomanager Instance { get; private set; }
    [Export] AudioStreamPlayer2D sfxPlayer, musicPlayer;
    [Export] Godot.Collections.Array<AudioStream> soundFX;

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
        var musicPlayback = musicPlayer.GetStreamPlayback();
        if (musicPlayback != null && musicPlayback is AudioStreamPlaybackInteractive pbi)
        pbi.SwitchToClipByName("Credits");
    }

    public void PlaySound(int i)
    {
        // 0 = paper Rustle; 1 = nose Placed; 2 = nose fell; 3 = powder applied;
        sfxPlayer.Stream = soundFX[i];
        sfxPlayer.Play();
    }
}