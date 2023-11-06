extends TextureButton

func _on_cell_pressed():
	EventBus.emit_signal("inventory_cell_choosed", self)


func _on_cell_mouse_exited():
	EventBus.emit_signal("inventory_cell_choosed", null)
