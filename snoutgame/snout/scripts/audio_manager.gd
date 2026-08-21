class_name AudioManager
extends Node

@export var sfx: Dictionary[String, AudioStream]
var sfx_player: AudioStreamPlayer
var sfx_playback: AudioStreamPlaybackPolyphonic
var music_player: AudioStreamPlayer
var atmo_player: AudioStreamPlayer
var atmo_playback: AudioStreamPlayback
static var ref: AudioManager
var wind_id: int
var rustle_id: int

func _ready() -> void:
	%AudioStreamPlayer.play()
	%AtmoStreamPlayer.play()
	sfx_player = %AudioStreamPlayer
	sfx_playback = %AudioStreamPlayer.get_stream_playback()
	music_player = %MusicStreamPlayer
	atmo_player = %AtmoStreamPlayer
	atmo_playback = %AtmoStreamPlayer.get_stream_playback()
	play_atmo(false)

func _on_new_line() -> void:
	sfx_playback.play_stream(sfx["impact_light"])

func play_atmo(is_final_level: bool) -> void:
	wind_id = atmo_playback.play_stream(sfx["wind_loop"])
	if !is_final_level:
		rustle_id = atmo_playback.play_stream(sfx["rustle_loop"])

func pause_atmo() -> void:
	atmo_playback.stop_stream(wind_id)
	atmo_playback.stop_stream(rustle_id)

#region singleton
func _singleton_check() -> void:
	if ref:
		queue_free()
		return
	ref = self

func _enter_tree() -> void:
	_singleton_check()
#endregion
