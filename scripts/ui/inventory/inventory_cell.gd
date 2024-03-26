extends Node2D
var empty = true
var object = null


func is_empty():
	return empty


func get_pos():
	return $object_pos.global_position
	

func _process(delta):
	$Label.text = str(empty)


func _on_cell_area_entered(area):
	print(area.name)
	if area.name == "object":
		empty = false
		if object == null:
			area.get_parent().set_cell_pos(get_pos())
			object = area.get_parent()
			return
		object.set_cell_pos(area.get_parent().get_cell_pos())
		area.get_parent().set_cell_pos(get_pos())


func _on_cell_area_exited(area):
	print(area.get_overlapping_areas())
	if area.name == "object" and $cell.get_overlapping_areas().size() == 0:
		empty = true
		object = null
