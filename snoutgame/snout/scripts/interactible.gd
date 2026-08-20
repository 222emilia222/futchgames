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
var is_cleared = false

func _ready() -> void:
	root_node = get_tree().root.get_child(1)
	player = root_node.get_child(0)
	camera = player.get_child(0)
	target_rotation_cam = Vector3(interact_angle.x,0,0)
	target_rotation_player = Vector3(0,interact_angle.y,0)

func interact() -> void:
	print("interacted with " + name)
	root_node.last_interacted = self
	if is_path:
		#load new environment
		root_node.load_scene(scene_name)
	else:
		#trigger corresponding dialogue, implement cue 
		DialogueManager.show_dialogue_balloon(dialogue, "start")
		#trigger poi close animation
		%Animator.play("close")
		%Animator.hide()
		%CollisionShape3D.disabled = true
		#camera adjustment
		player.lerp_fov(interact_fov)
		var tween := create_tween()
		tween.set_parallel()
		tween.tween_property(camera.get_parent(), "rotation_degrees", target_rotation_player, root_node.camera_adj_speed)
		tween.tween_property(camera, "rotation_degrees", target_rotation_cam, root_node.camera_adj_speed)

func after_interaction() -> void:
	if !is_cleared:
		#trigger poi open animation
		%Animator.play("open")
		%Animator.show()
		%CollisionShape3D.disabled = false
