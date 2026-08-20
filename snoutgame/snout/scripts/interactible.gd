extends Node

@export var is_path: bool
@export var scene_name: String
@export var dialogue: DialogueResource
@export var cue_name: String
@export var interact_fov: float
@export var interact_angle: Vector2
var root_node: Node3D
var player: Node3D
var camera: Camera3D
var target_rotation_player: Vector3
var target_rotation_cam: Vector3

func _ready() -> void:
	root_node = get_tree().root.get_child(1)
	player = root_node.get_child(0)
	camera = player.get_child(0)
	target_rotation_cam = Vector3(interact_angle.x,0,0)
	target_rotation_player = Vector3(0,interact_angle.y,0)

func interact() -> void:
	print("interacted with " + name)
	if is_path:
		#load new environment
		root_node.load_scene(scene_name)
	else:
		#trigger corresponding dialogue, implement cue 
		DialogueManager.show_dialogue_balloon(dialogue, "start")
		player.lerp_fov(interact_fov)
		var tween := create_tween()
		tween.set_parallel()
		tween.tween_property(camera.get_parent(), "rotation_degrees", target_rotation_player, root_node.camera_adj_speed)
		tween.tween_property(camera, "rotation_degrees", target_rotation_cam, root_node.camera_adj_speed)
