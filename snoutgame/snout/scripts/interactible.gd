extends Node

@export var is_path: bool
@export var scene_name: String
@export var dialogue: DialogueResource
@export var dialogue_cue: String
@export var interact_fov: float
@export var interact_angle: Vector2
var is_interacting: bool
var root_node: Node

func _ready() -> void:
	root_node = get_tree().root.get_child(1)

func interact() -> void:
	print("interacted with " + name)
	if is_path:
		#load new environment
		get_tree().root.get_child(0).load_scene(scene_name)
	else:
		#trigger corresponding dialogue, implement cue 
		DialogueManager.show_dialogue_balloon(dialogue, "start")
		is_interacting = true

func _physics_process(delta: float) -> void:
	pass
	#if is_interacting: #after interacting
		#$Camera.rotation.y = $Camera.rotation.y.lerp(interact_angle.y, delta * root_node.camera_adj_speed)
		#$Camera.rotation.x = $Camera.rotation.x.lerp(interact_angle.y, delta * root_node.camera_adj_speed)
		#$Camera.fov = $Camera.fov.lerp(interact_fov, delta * root_node.camera_adj_speed)
	#else: #after stop interacting
		#$Camera.fov = $Camera.fov.lerp(interact_fov, delta * root_node.game_fov)
