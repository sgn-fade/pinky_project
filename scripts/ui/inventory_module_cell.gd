extends TextureButton
var cell_index = null
func _on_cell_pressed():
	EventBus.emit_signal("inventory_cell_choosed", self)
