extends Node2D
var empty = true
var object = null
var slot_type = "inventory"

func is_empty():
	return empty


func get_pos():
	return $object_pos.global_position
	


func _on_cell_area_entered(area):
	if area.name == "object":
		area.get_parent().set_target_cell(self)


func swap_objects(prev_cell, new_object):
	if object != null:
		object.set_cell(prev_cell)
		prev_cell.set_object(object)
		set_object(new_object)
		return
	prev_cell.clear()
	set_object(new_object)


func set_object(new_object):
	object = new_object
	empty = false
	

func clear():
	empty = true
	object = null

func _on_cell_area_exited(area):
	if area.name == "object":
		area.get_parent().set_target_cell(null)
