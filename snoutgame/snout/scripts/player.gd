extends Node3D

@export var rotation_speed: float
var seeing_interactible: bool

func _ready() -> void:
	pass # Replace with function body.

func _process(_delta: float) -> void:
	pass

func _unhandled_input(_event: InputEvent) -> void:
	var dir_vect = Input.get_vector("down", "up", "right", "left")
	rotate_y(dir_vect.y * rotation_speed)
	%Camera.rotate_x(dir_vect.x * rotation_speed)
	if Input.is_action_just_pressed("action") and seeing_interactible:
		%SeeCast.get_collider().get_parent().interact()

func _physics_process(_delta: float) -> void:
	%InteractHint.hide()
	seeing_interactible = false
	#%SeeCast.target_position = %Camera3D
	if %SeeCast.is_colliding():
		var target = %SeeCast.get_collider()
		if target != null and target.get_parent().has_method("interact"):
			%InteractHint.show()
			seeing_interactible = true
