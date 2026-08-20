extends Node

@export var is_path: bool
@export var scene_name: String
@export var dialogue: DialogueResource
@export var dialogue_cue: String
@export var interact_fov: float
@export var interact_angle: Vector2
var root_node: Node3D
var camera: Camera3D
var target_rotation_cam = Vector3(interact_angle.x,0,0)
var target_rotation_player = Vector3(0,interact_angle.y,0)

func _ready() -> void:
	root_node = get_tree().root.get_child(1)
	camera = root_node.get_child(0).get_child(0)

func interact() -> void:
	print("interacted with " + name)
	if is_path:
		#load new environment
		root_node.load_scene(scene_name)
	else:
		#trigger corresponding dialogue, implement cue 
		DialogueManager.show_dialogue_balloon(dialogue, "start")
		var tween := create_tween()
		tween.set_parallel()
		tween.tween_property(camera, "fov", interact_fov, (root_node.game_fov - interact_fov) * root_node.camera_adj_speed / 2)
		tween.tween_property(camera.get_parent(), "rotation", target_rotation_player, root_node.camera_adj_speed)
		tween.tween_property(camera, "rotation", target_rotation_cam, root_node.camera_adj_speed)

		#var tween := create_tween()
		#tween.tween_property(camera, "fov", root_node.game_fov, (root_node.fov - interact_fov) * root_node.camera_adj_speed)
