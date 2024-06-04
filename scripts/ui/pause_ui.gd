extends Control

func _on_continue_pressed():
	Input.action_press("ui_cancel")


func _on_option_pressed():
	pass


func _on_quit_pressed():
	get_tree().quit()


