extends Node2D


var array : Array[Vector2i] = []

func get_size():
	return $room_area/size_rect.shape.get_rect().size

func get_center():
	return global_position

func create_tonel(direction):
	fill_array(direction)
	$tileset.set_cells_terrain_connect(0, array, 0, 0)

func fill_array(direction):
	var y_direction = 0
	var x_direction = 0
	
	if direction.x != 0:
		x_direction = direction.x
		y_direction = 1
	if direction.y != 0:
		y_direction = direction.y
		x_direction = 1
	for y in 2 + abs(direction.y) * 10:
		for x in 2 + abs(direction.x) * 10:
			array.append(Vector2i((x - 1) * sign(x_direction), (y - 1) * sign(y_direction)))
