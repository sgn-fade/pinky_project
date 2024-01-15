extends TextureButton


func _input(event):
	if Input.is_action_pressed("mouse_left_button"):
		position = get_global_mouse_position()
	else:
		position = get_parent().position

func _on_pressed():
	position = get_global_mouse_position()
