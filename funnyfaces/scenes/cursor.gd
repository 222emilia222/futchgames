extends Node2D

var openHand = preload("res://hand_open.png")
var pointedHand = preload("res://hand_closed.png")
var radiusBig = preload("res://hand_closed.png") ##clownnose and white powder
var radiusMid = preload("res://hand_closed.png") ##color
var radiusSmall = preload("res://hand_closed.png") ##lipstick and kayal

func _ready():
	Input.set_custom_mouse_cursor(openHand, Input.CURSOR_ARROW, Vector2(16,16))
func _on_interactible_mouse_entered():
	Input.set_custom_mouse_cursor(pointedHand, Input.CURSOR_ARROW, Vector2(16,16))
func _on_interactible_mouse_exited():
	Input.set_custom_mouse_cursor(openHand, Input.CURSOR_ARROW, Vector2(16,16))
