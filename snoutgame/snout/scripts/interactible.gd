extends Node

@export var is_path: bool
@export var key_name: String

func interact() -> void:
	print("interacted with " + name)
	if is_path:
		#load new environment
		get_tree().root.get_child(0).load_scene(key_name)
	else:
		#trigger corresponding dialogue
		DialogueManager.show_dialogue_balloon(get_tree().root.get_child(0).dialogues[key_name], "start")
