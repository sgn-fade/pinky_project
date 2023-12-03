extends Control



func _on_play_pressed():
	EventBus.emit_signal("load_game")
	visible = false


func _on_quit_pressed():
	get_tree().quit()
