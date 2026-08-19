extends Node

@export var is_path: bool
@export var scene_name: String
@export var dialogue: DialogueResource
@export var dialogue_cue: String
@export var interact_fov: float
@export var interact_angle: Vector2

func interact() -> void:
	print("interacted with " + name)
	if is_path:
		#load new environment
		get_tree().root.get_child(0).load_scene(scene_name)
	else:
		#trigger corresponding dialogue, implement cue 
		DialogueManager.show_dialogue_balloon(dialogue, "start")
		#camera rotation and fov

func _physics_process(delta: float) -> void:
	if false: #after interacting
		$Camera3D.rotation.y = $Camera3D.rotation.y.lerp(interact_angle.y, delta * %Game.camera_adj_speed)
		$Camera3D.rotation.x = $Camera3D.rotation.x.lerp(interact_angle.y, delta * %Game.camera_adj_speed)
		$Camera3D.fov = $Camera3D.fov.lerp(interact_fov, delta * %Game.camera_adj_speed)
	if false: #after stop interacting
		$Camera3D.fov = $Camera3D.fov.lerp(interact_fov, delta * %Game.game_fov)
