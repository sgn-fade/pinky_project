extends "res://scripts/ui/inventory/inventory_cell.gd"


func set_object(new_object):
	if new_object.data_type == "weapon":
		object = new_object
		empty = false
		EventBus.emit_signal("switch_hands_stance", new_object.data)
		Player.set_weapon(new_object.data)

func _on_cell_area_entered(area):
	if area.name == "object" and area.get_parent().data_type == "weapon":
		area.get_parent().set_target_cell(self)


func clear():
	empty = true
	object = null
	print(clear)
	EventBus.emit_signal("switch_hands_stance", null)
	Player.set_weapon(null)
	
