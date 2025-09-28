using Godot;
using System;
using System.Threading.Tasks;

public partial class Audiomanager : Control
{
    public static Audiomanager Instance { get; private set; }
    [Export] AudioStream creditsMusic;
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
    public async Task SwitchToCreditsMusic()
    {
        Tween tween = GetTree().CreateTween().SetEase(Tween.EaseType.In);
        tween.TweenProperty(musicPlayer, "volume_db", -40, 4.5f);
        await ToSignal(GetTree().CreateTimer(5), Godot.Timer.SignalName.Timeout);
        musicPlayer.Stop();
        musicPlayer.VolumeDb = 0.0f;
        musicPlayer.Stream = creditsMusic;
        musicPlayer.Play();
    }

    public void PlaySound(int i)
    {
        // 0 = paper Rustle; 1 = nose Placed; 2 = nose fell; 3 = powder applied;
        sfxPlayer.Stream = soundFX[i];
        sfxPlayer.Play();
    }
}