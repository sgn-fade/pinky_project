extends "res://scripts/ui/inventory/inventory_cell.gd"
signal button_pressed
signal set_weapon_to_ui(weapon)


func _ready():
	slot_type = "weapon"

func set_object(new_object):
	if new_object.data_type == "weapon":
		object = new_object
		empty = false
		EventBus.emit_signal("switch_hands_stance", new_object.data)
		Player.set_weapon(new_object.data)	
		emit_signal("set_weapon_to_ui", new_object.data)


func _on_cell_area_entered(area):
	if area.name == "object" and area.get_parent().data_type == "weapon":
		area.get_parent().set_target_cell(self)


func clear():
	empty = true
	object = null
	EventBus.emit_signal("switch_hands_stance", null)
	Player.set_weapon(null)
	emit_signal("set_weapon_to_ui", null)


func _on_texture_button_pressed():
	emit_signal("button_pressed")
