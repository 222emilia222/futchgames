extends Node2D

var openHand = preload("res://sprites/cursors/hand_open.png")
var pointedHand = preload("res://sprites/cursors/hand_pointing.png")
var radiusBig = preload("res://sprites/cursors/radius_NOSE.png") ##clownnose
var radiusMid = preload("res://sprites/cursors/radius_MEDIUM.png") ##color and white powder
var radiusSmall = preload("res://sprites/cursors/radius_SMALL.png") ##lipstick and kayal

func _ready():
	Input.set_custom_mouse_cursor(openHand, Input.CURSOR_ARROW)
func _on_interactible_mouse_entered():
	Input.set_custom_mouse_cursor(pointedHand, Input.CURSOR_ARROW)
func _on_interactible_mouse_exited():
	Input.set_custom_mouse_cursor(openHand, Input.CURSOR_ARROW)
