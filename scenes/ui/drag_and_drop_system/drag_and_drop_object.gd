extends TextureButton
signal mouse_entered_to_destionation
signal mouse_exit_from_destionation
signal button_selected



func _get_drag_data(at_position):
	var preview_texture = TextureRect.new()
	preview_texture.texture = texture_normal
	preview_texture.expand_mode = 1
	preview_texture.size = Vector2(90,90)
	preview_texture.position = Vector2(-60, -60)
	var preview = Control.new()
	preview.add_child(preview_texture)
 
	set_drag_preview(preview)
	texture_normal = null
 
	return preview_texture.texture


func _on_pressed():
	emit_signal("button_selected", self)


func _on_mouse_entered():
	emit_signal("mouse_entered_to_destionation", self)


func _on_mouse_exited():
	emit_signal("mouse_exit_from_destionation")
