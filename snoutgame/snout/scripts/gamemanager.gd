extends Node3D

var current_scene: Node3D
@export var game_fov: float
@export var camera_adj_speed: float
@export var places: Dictionary[String, String]
@export var intro: DialogueResource
var last_interacted: Node3D
var sound: AudioManager
var is_final_level = false
var in_intro = true

func _ready() -> void:
	var start_scene = load(places["start"])
	var instance = start_scene.instantiate()
	add_child(instance)
	current_scene = instance
	%Camera.fov = game_fov
	DialogueManager.dialogue_ended.connect(_on_dialogue_ended)
	sound = AudioManager.ref
	DialogueManager.show_dialogue_balloon(intro, "start")

func _input(_event: InputEvent) -> void:
	if Input.is_action_just_pressed("lake_shortcut"):
		load_scene("lake")

func load_scene(key: String) -> void:
	if places.has(key):
		current_scene.queue_free()
		#transition
		var footstep_key = "footsteps"
		if key == "lake":
			print("got to the lake")
			is_final_level = true
			footstep_key = "footsteps_running"
		else:
			sound.pause_atmo()
		sound.sfx_playback.play_stream(sound.sfx[footstep_key])
		await get_tree().create_timer(sound.sfx[footstep_key].get_length() + 0.1).timeout
		#after transition
		var scene = load(places[key])
		var instance = scene.instantiate()
		add_child(instance)
		current_scene = instance
		sound.play_atmo(is_final_level)
	else:
		print("scene key (" + key + ") not found in places array")

func _on_dialogue_ended(_resource: DialogueResource) -> void:
	get_child(0).lerp_fov(game_fov)
	sound.sfx_playback.play_stream(sound.sfx["impact_heavy"])
	if in_intro:
		in_intro = false
		return
	last_interacted.after_interaction()
