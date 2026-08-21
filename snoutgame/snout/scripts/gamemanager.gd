extends Node3D

var current_scene: Node3D
@export var game_fov: float
@export var camera_adj_speed: float
@export var places: Dictionary[String, String]
var last_interacted: Node3D
var sound: AudioManager
var is_final_level = false
var next_scene_key: String

func _ready() -> void:
	var start_scene = load(places["start"])
	var instance = start_scene.instantiate()
	add_child(instance)
	current_scene = instance
	%Camera.fov = game_fov
	DialogueManager.dialogue_ended.connect(_on_dialogue_ended)
	sound = AudioManager.ref

func _input(_event: InputEvent) -> void:
	if Input.is_action_just_pressed("lake_shortcut"):
		load_scene("lake")

func load_scene(key: String) -> void:
	if places.has(key):
		current_scene.queue_free()
		#transition
		sound.atmo_player.stop()
		next_scene_key = key
		sound.sfx_playback.play_stream(sound.sfx["footsteps"])
		sound.sfx_player.finished.connect(_on_transition_finished)
	else:
		print("scene key (" + key + ") not found in places array")

func _on_transition_finished() -> void:
	#after transition
	var scene = load(places[next_scene_key])
	var instance = scene.instantiate()
	add_child(instance)
	current_scene = instance
	sound.play_atmo(is_final_level)

func _on_dialogue_ended(_resource: DialogueResource) -> void:
	get_child(0).lerp_fov(game_fov)
	last_interacted.after_interaction()
	sound.sfx_playback.play_stream(sound.sfx["impact_heavy"])
