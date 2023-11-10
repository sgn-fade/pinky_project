extends Node2D

var light = preload("res://scenes/Lights.tscn")
var goblin = preload("res://scenes/goblin.tscn")
var goblin_mage = preload("res://scenes/enemies/goblins/goblin_mage.tscn")
var box = preload("res://scenes/box.tscn")
var altar = preload("res://scenes/altar.tscn")
var grass = preload("res://scenes/grass.tscn")
var modules_drop = load("res://scenes/modules_drop.tscn")
var fire_elemental = load("res://scenes/enemies/elementals/fire_elemental.tscn")
var portal = load("res://scenes/world_env/portal.tscn")


var tile_size = 64
var min_size = 2
var max_size = 1
var room_count
var room_generate_count = 2
onready var Map = $TileMap
onready var Grass = $grass
var timer := Timer.new()

func _ready():
	Player.set_position(Vector2.ZERO)
	EventBus.connect("survive_event_started", self, "_on_survive_event_started")
	EventBus.connect("enemy_killed", self, "_on_enemy_killed")
	add_child(timer)
	timer.one_shot = false
	randomize()

func _on_enemy_killed():
	if $mobs.get_children().size() == 1:
		spawn_portal()


func spawn_portal():
	var object = portal.instance()
	add_child(object)
	object.global_position = Player.get_position() + Vector2(randi()%20, randi()%20)
	
func generate_dungeon():
	create_center_room()
	Player.set_position(Vector2.ZERO)
func create_empty_space(height, width, center):
	for x in range(center.x - width - 5, center.x + width + 5):
		for y in range(center.y - height - 5, center.y + height + 5):
			if Map.get_cell(x, y) == -1:
				Map.set_cell(x, y, 0)
				Map.tile_set.autotile_set_bitmask(1, Vector2(x, y), TileSet.BIND_CENTER)


func generate(height, width, center):
	spawn_goblin(center * tile_size, height, width)
	create_empty_space(height, width, center)
	#spawn_light(center, height, width)
	#spawn_grass(center * tile_size, height * tile_size, width * tile_size)
	for x in range(center.x - width, center.x + width):
		for y in range(center.y - height, center.y + height):
			Map.set_cell(x, y, 1)
			Map.tile_set.autotile_set_bitmask(1, Vector2(x, y), TileSet.BIND_CENTER)
	Map.update_bitmask_region()
	return Vector2(width, height)


func spawn_module(center,height, width):
	var modules_drop1 = modules_drop.instance()
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
	var x = -width / 2
	for i in 2:
		var y = -height / 2
		for j in 2:
			var light1 = light.instance()
			add_child(light1)
			light1.global_position = (center + Vector2(x, y)) * tile_size
			light1.z_index = light1.global_position.y / 2
			y += height
		x += width


func spawn_goblin(coord, height, width):
	if coord == Vector2.ZERO:
		return
	for i in 4:
		random_mob_instance(coord, height, width)


func random_mob_instance(coord, height, width):
	var mob
	match randi() % 2 + 1:
		1:
			mob = goblin.instance()
		2:
			mob = goblin_mage.instance()
	$mobs.add_child(mob)
	mob.global_position = Vector2(coord.x + (randi() % int(width) * 2 - int(width)) * (tile_size / 2.0) , 
												  coord.y + (randi() % int(height) * 2 - int(height)) * (tile_size / 2.0))


func create_center_room():
	room_count = room_generate_count
	room_count -= 1
	var room_direction
	var room_width = randi() % max_size + min_size
	var room_height = randi() % (max_size) + min_size
	var room_size = generate(room_height, room_width, Vector2(0, 0))
	var room_center = Vector2(0, 0)
	match randi()%4 + 1:
		1:room_direction = Vector2(-1, 0)
		2:room_direction = Vector2(1, 0)
		3:room_direction = Vector2(0, -1)
		4:room_direction = Vector2(0, 1)
	create_room(room_size, room_center, room_direction)


func _on_survive_event_started(room_size, room_center, survive_time):
	for i in survive_time / 10:
		timer.start(1)
		yield(timer, "timeout")
		spawn_goblin(room_center, room_size.y, room_size.x)


func create_room(room_size, room_center, room_direction):
	room_count -= 1
	if room_count < 0:
		return
	var tonel_size = randi()% 2 + 2
	var room_width = randi() % max_size + min_size
	var room_height = randi() % (max_size) + min_size
	var current_room_center = room_center
	var tonel_offset = randi() % 7 - 3
	
	for i in range(0, tonel_size):
		Map.set_cell(room_center.x + (room_size.x + i) * room_direction.x ,
					room_center.y + (room_size.y + i) * room_direction.y, 1)
		Map.tile_set.autotile_set_bitmask(1, Vector2(room_size.x, room_size.y), TileSet.BIND_CENTER)
	current_room_center = Vector2(room_center.x + (room_size.x + tonel_size + room_width) * room_direction.x, #еще раз пересмотреть позже)
								room_center.y + (room_size.y + tonel_size + room_height) * room_direction.y)
	Map.update_bitmask_region()
	if Map.get_cell(current_room_center.x, current_room_center.y) == 1:
		room_direction = generate_direction(room_direction)
		create_room(room_size, room_center, room_direction)
		return
	room_size = generate(room_height, room_width, current_room_center)
	room_direction = generate_direction(room_direction)
	create_room(room_size, current_room_center, room_direction)


func generate_direction(previous_direction):
	var room_direction
	match randi()%4 + 1:
		1:room_direction = Vector2(-1, 0)
		2:room_direction = Vector2(1, 0)
		3:room_direction = Vector2(0, -1)
		4:room_direction = Vector2(0, 1)
	if previous_direction + room_direction == Vector2.ZERO:
		return generate_direction(previous_direction)
	return room_direction





