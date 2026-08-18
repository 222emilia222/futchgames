extends Node3D

@export var rotation_speed: float

func _ready() -> void:
	pass # Replace with function body.

func _process(_delta: float) -> void:
	pass

func _unhandled_input(_event: InputEvent) -> void:
	var dir_vect = Input.get_vector("down", "up", "right", "left")
	rotate_y(dir_vect.y * rotation_speed)
	%Camera3D.rotate_x(dir_vect.x * rotation_speed)

func _physics_process(_delta: float) -> void:
	%InteractHint.hide()
	if %SeeCast.is_colliding():
		var target = %SeeCast.get_collider()
		if target!= null and target.has_method("interact"):
			%InteractHint.show()
			if Input.is_action_just_pressed("action"):
				target.interact()
