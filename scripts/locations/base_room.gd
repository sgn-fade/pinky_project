extends Node2D




func get_size():
	return $room_area/size_rect.shape.get_rect().size

func get_center():
	return global_position
