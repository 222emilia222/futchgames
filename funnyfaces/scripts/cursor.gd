extends Node2D

var openHand = preload("res://sprites/cursors/hand_open.png")
var pointedHand = preload("res://sprites/cursors/hand_pointing.png")
var radiusBig = preload("res://sprites/cursors/radius_NOSE.png") ##clownnose
var radiusMid = preload("res://sprites/cursors/radius_MEDIUM.png") ##color and white powder
var radiusSmall = preload("res://sprites/cursors/radius_SMALL.png") ##lipstick and kayal
var current

func _ready():
	current = openHand
	Input.set_custom_mouse_cursor(openHand, Input.CURSOR_ARROW, Vector2(32,32))
func _on_interactible_mouse_entered():
	Input.set_custom_mouse_cursor(pointedHand, Input.CURSOR_ARROW, Vector2(32,32))
func _on_interactible_mouse_exited():
	Input.set_custom_mouse_cursor(current, Input.CURSOR_ARROW, Vector2(32,32))

func _set_cursor(key: int):
	match key:
		0:
			current=openHand
		1:
			current=radiusSmall
		2:
			current=radiusMid
		3:
			current=radiusBig
	Input.set_custom_mouse_cursor(current, Input.CURSOR_ARROW, Vector2(32,32))

func _set_cursor_vis(vis: bool):
	print("in vis method")
	if (vis == false):
		Input.set_mouse_mode(Input.MOUSE_MODE_HIDDEN)
	elif (vis == true):
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		print("set visible")
