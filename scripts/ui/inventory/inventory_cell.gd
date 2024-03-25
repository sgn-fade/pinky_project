extends Node2D
var empty = true

func is_empty():
	return empty


func get_pos():
	return $object_pos.global_position


func _on_object_entered(area):
	if area.name == "object":
		area.get_parent().set_cell_pos(get_pos())


