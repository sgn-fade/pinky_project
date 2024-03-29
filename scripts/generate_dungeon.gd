extends Node2D

var light = preload("res://scenes/Lights.tscn")
var goblin = preload("res://scenes/enemies/goblins/goblin_melee.tscn")
var goblin_mage = preload("res://scenes/enemies/goblins/goblin_mage.tscn")
var skeleton = preload("res://scenes/enemies/undeads/skeleton.tscn")
var box = preload("res://scenes/box.tscn")
var altar = preload("res://scenes/altar.tscn")
var grass = preload("res://scenes/grass.tscn")
var modules_drop = load("res://scenes/modules_drop.tscn")
var fire_elemental = load("res://scenes/enemies/elementals/fire_elemental.tscn")
var portal = load("res://scenes/world_env/portal.tscn")
var fog_particles = load("res://scenes/particles/fog.tscn")

var room = load("res://scenes/locations/base_room.tscn")
var rooms = []
var tile_size = 64
var min_size = 2
var max_size = 2
var room_count

@onready var tile_map = $tileset2
@export var room_generate_count = 7


var timer := Timer.new()

func _on_enemy_killed():
	if $mobs.get_children().size() == 1:
		spawn_portal()


func spawn_portal():
	var object = portal.instantiate()
	add_child(object)
	object.global_position = Player.get_position() + Vector2(randi()%20, randi()%20)
	


func spawn_module(center,height, width):
	var modules_drop1 = modules_drop.instantiate()
	add_child(modules_drop1)
	modules_drop1.global_position = (center + Vector2(randi()%50, randi()%50)) 

func spawn_grass(center, height, width):
	for x in range(-width, width, randi()%1 + 10):
		for y in range(-height, height, randi()%1 + 10):
			if randf() > 0.5:
				var grass_instance = grass.instance()
				add_child(grass_instance)
				grass_instance.global_position = Vector2((center.x + x), center.y + y)


func spawn_light(center, height, width):
	for x in range(-width/2, width/2, width):
		for y in range(-height/2, height/2, width/2):
			var light1 = light.instantiate()
			add_child(light1)
			light1.global_position = (center + Vector2(x, y)) * tile_size
			light1.z_index = light1.global_position.y / 2


func spawn_fog(center, height, width):
	for x in range(-width * tile_size + 20, width * tile_size,  (randi()%70 + 50)):
		for y in range(-height  * tile_size + 20, height * tile_size, (randi()%70 + 50)):
			var fog = fog_particles.instantiate()
			add_child(fog)
			fog.global_position = (center * tile_size + Vector2(x, y))
			fog.z_index = 1024


func spawn_goblin(room, room_position):
	for i in 8:
		random_mob_instance(room, room_position)
		await get_tree().create_timer(0.1).timeout


func random_mob_instance(room, room_position):
	randomize()
	var mob
	var local_coord = Vector2i(randi()%room.get_size().x * 64, randi()%room.get_size().y * 64)
	var global_coord = room_position * 64 + local_coord
	
	if BetterTerrain.get_cell(tile_map, 0, global_coord / 64) != 1:
		return
	match 1:#randi() % 3 + 1:
		1:
			mob = goblin.instantiate()
		2:
			mob = goblin_mage.instantiate()
		3:
			mob = skeleton.instantiate()
	add_child(mob)
	mob.global_position = global_coord




#----------------------------------------------------------------
func generate_direction(previous_direction):
	var room_direction
	match randi()%4 + 1:
		1:room_direction = Vector2(-1, 0)
		2:room_direction = Vector2(1, 0)
		3:room_direction = Vector2(0, -1)
		4:room_direction = Vector2(0, 1)
	if previous_direction + room_direction == Vector2.ZERO or previous_direction == room_direction:
		return generate_direction(previous_direction)
	return room_direction




func _ready():
	center_room()
	for i in 3:
		var distance = Vector2.ZERO
		var direction = Vector2.ZERO
		for j in 3:
			var b_room = room.instantiate()
			add_child(b_room)
			#rooms.append(b_room)
			direction = await place_room(b_room, direction, distance)
			distance = b_room.global_position
	var size = tile_map.get_used_rect().size
	for x in range(-size.x/2, size.x / 2):
		for y in range(-size.y/2, size.y / 2):
			if tile_map.get_cell_source_id(1, Vector2i(x, y)) == 3:
				BetterTerrain.update_terrain_cell(tile_map, 1, Vector2i(x, y), false) 
	#BetterTerrain.update_terrain_area(tile_map, 1, , false)

func center_room():
	var center_room = room.instantiate()
	add_child(center_room)
	center_room.global_position = Vector2.ZERO
	var pattern = tile_map.tile_set.get_pattern(0)
	tile_map.set_pattern(1, Vector2i.ZERO - pattern.get_size() / 2, pattern)


func place_room(room, prev_direction, prev_distance):
	var direction = generate_direction(prev_direction)
	var distance = prev_distance + direction * 700
	room.global_position = distance 
	await get_tree().create_timer(0.1).timeout
	if room.get_node("room_area").has_overlapping_areas():
		return await place_room(room, prev_direction, distance)
	var pattern = tile_map.tile_set.get_pattern(randi()%8)
	var pattern_position = Vector2i(distance / 64) - pattern.get_size() / 2
	tile_map.set_pattern(1, pattern_position, pattern)
	create_tonel(-direction, room)
	#spawn_goblin(pattern, pattern_position)
	return direction

func create_tonel(direction, room):
	fill_array(direction, room)
	BetterTerrain.set_cells(tile_map, 1, array, 1)
	array.clear()

var array : Array[Vector2i] = []

func fill_array(direction, room):
	var y_direction = 0
	var x_direction = 0
	
	if direction.x != 0:
		x_direction = direction.x
		y_direction = 1
	if direction.y != 0:
		y_direction = direction.y
		x_direction = 1
	for y in 1 + abs(direction.y) * 10:
		for x in 1 + abs(direction.x) * 10:
			array.append(Vector2i(room.global_position / 64) + Vector2i((x) * sign(x_direction), (y) * sign(y_direction)))

func _input(event):
	if Input.is_action_just_pressed("Space"):
		get_tree().reload_current_scene()
