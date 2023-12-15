extends TextureButton
var cell_index = null
var timer = Timer.new()
func _ready():
	add_child(timer)
	timer.one_shot = false
	EventBus.connect("spell_slot_button_choosed", self, "_on_spell_slot_button_choosed")
	EventBus.connect("spell_cells_light_off", self, "spell_cells_light_off")
	
func _on_cell_pressed():
	EventBus.emit_signal("inventory_cell_choosed", self)
func _on_spell_slot_button_choosed(slot, equiped):
	if equiped or $light.scale.x >= 0.1:
		return
	while($light.scale.x < 0.12):
		$light.scale.x += 0.01
		$light.scale.y += 0.01
		timer.start(0.01)
		yield(timer,"timeout")

func spell_cells_light_off():
	while($light.scale.x > 0):
		timer.start(0.01)
		yield(timer,"timeout")
		$light.scale.x -= 0.01
		$light.scale.y -= 0.01
	
