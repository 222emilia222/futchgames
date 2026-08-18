extends Node3D

var current_scene: Node3D
@export var places: Dictionary[String, String]
@export var dialogues: Dictionary[String, String]

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var start_scene = load(places["start"])
	var instance = start_scene.instantiate()
	add_child(instance)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	pass

func load_scene(key: String) -> void:
	if places.has(key):
		current_scene.queue_free()
		var scene = load(places[key])
		add_child(scene)
		current_scene = scene
