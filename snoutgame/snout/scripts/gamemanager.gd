extends Node3D

var current_scene: Node3D
@export var game_fov: float
@export var camera_adj_speed: float
@export var places: Dictionary[String, String]

func _ready() -> void:
	var start_scene = load(places["start"])
	var instance = start_scene.instantiate()
	add_child(instance)
	%Camera.fov = game_fov
	DialogueManager.dialogue_ended.connect(_on_dialogue_ended)

func _process(_delta: float) -> void:
	pass

func load_scene(key: String) -> void:
	if places.has(key):
		current_scene.queue_free()
		#vllt process frame hier
		var scene = load(places[key])
		add_child(scene)
		current_scene = scene

func _on_dialogue_ended(_resource: DialogueResource) -> void:
	print("on dialogue ended signal got through")
	get_child(0).lerp_fov(game_fov)
