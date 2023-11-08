extends TextureButton
var cell_index = null
var timer = Timer.new()
func _ready():
	add_child(timer)
	timer.one_shot = false
	EventBus.connect("spell_slot_button_choosed", self, "_on_spell_slot_button_choosed")
	EventBus.connect("inventory_cell_choosed", self, "_on_inventory_cell_choosed")
	
func _on_cell_pressed():
	EventBus.emit_signal("inventory_cell_choosed", self)
func _on_spell_slot_button_choosed(slot, equiped):
	
if equiped or $light.scale.x >= 0.1:
		return
	for i in 5:

		$light.scale.x += 0.02
		$light.scale.y += 0.02
		timer.start(0.02)
		yield(timer,"timeout")

func _on_inventory_cell_choosed(cell):

	for i in 5:
		timer.start(0.02)
		yield(timer,"timeout")
		$light.scale.x -= 0.02
		$light.scale.y -= 0.02
	
