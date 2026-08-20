extends Node3D

var current_scene: Node3D
@export var game_fov: float
@export var camera_adj_speed: float
@export var places: Dictionary[String, String]
var last_interacted: Node3D

func _ready() -> void:
	var start_scene = load(places["start"])
	var instance = start_scene.instantiate()
	add_child(instance)
	current_scene = instance
	%Camera.fov = game_fov
	DialogueManager.dialogue_ended.connect(_on_dialogue_ended)

func _process(_delta: float) -> void:
	pass

func load_scene(key: String) -> void:
	if places.has(key):
		current_scene.queue_free()
		#vllt process frame hier
		var scene = load(places[key])
		var instance = scene.instantiate()
		add_child(instance)
		current_scene = instance
	else:
		print("scene key (" + key + ") not found in places array")

func _on_dialogue_ended(_resource: DialogueResource) -> void:
	get_child(0).lerp_fov(game_fov)
	last_interacted.after_interaction()
