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

var tile_size = 64
var min_size = 2
var max_size = 2
var room_count

@export var room_generate_count = 7

@onready var Map = $TileMap
@onready var Grass = $grass
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


func spawn_goblin(coord, height, width):
	if coord == Vector2.ZERO:
		return
	for i in 8:
		random_mob_instance(coord, height, width)
		await get_tree().create_timer(0.1).timeout


func random_mob_instance(coord, height, width):
	randomize()
	var mob
	match 1:#randi() % 3 + 1:
		1:
			mob = goblin.instantiate()
		2:
			mob = goblin_mage.instantiate()
		3:
			mob = skeleton.instantiate()
	$mobs.add_child(mob)
	mob.global_position = coord
	mob.move_and_slide( (randi()%10) * Vector2(coord.x + (randi() % int(width) * 2 - int(width)) * (tile_size / 2.0) , 
												  coord.y + (randi() % int(height) * 2 - int(height)) * (tile_size / 2.0)))




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





