extends "res://scripts/ui/inventory/inventory_cell.gd"
var cell_index

func _ready():
	slot_type = "spell"


func _on_cell_area_entered(area):
	if area.name == "object" and area.get_parent().data_type == "spell":
		area.get_parent().set_target_cell(self)

func set_object(new_object):
	if new_object.data_type == "spell":
		object = new_object
		empty = false
		Player.get_weapon().add_module_to_weapon(object.data, cell_index)


func restore_object(old_object):
	object = old_object
	empty = false



func clear():
	super.clear()
	Player.get_weapon().remove_module_from_weapon(cell_index)
