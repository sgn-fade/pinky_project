extends Node
var hp = 50
var max_hp = 50
var coins = 0
onready var ui = get_node("/root/World/Ui")
onready var player_scene = load("res://scenes/main_character.tscn")
onready var player = null
var score = 0
var weapon = null
var closest_interactive_object = null
var magic_damage = 1
func _ready():
	EventBus.emit_signal("update_character_hp_bar_value", hp, max_hp)


func set_state(state):
	player.change_state(state)
func get_state():
	return player.current_state
func get_hp():
	return hp
func get_max_hp():
	return max_hp
func update_hp(value):
	hp += value


func get_position():
	return player.global_position
func set_position(position):
	player.global_position = position

func ready():
	if player != null:
		return true
	return false

func get_body():
	return player


func get_z_index():
	return player.z_index


func get_weapon():
	return weapon
func set_weapon(new_weapon):
	weapon = new_weapon


func set_magic_damage(new_magic_damage):
	magic_damage = new_magic_damage
func get_magic_damage():
	return magic_damage


func get_closest_object():
	return closest_interactive_object
func set_closest_object(object):
	if object == null:
		closest_interactive_object = null
		return true
	if get_closest_object() == null:
		closest_interactive_object = object
		return true
	if (object.global_position - get_position()).length() < (get_closest_object().global_position - get_position()).length():
		closest_interactive_object = object
		return true
	return false


func get_money():
	return coins

func set_money(value):
	if value > 0:
		coins += value
		score += value * 10

func get_score():
	return score
func set_score(value):
	if value <= 0:
		return
	score += value

func restart():
	player.queue_free()
	hp = max_hp
	player = player_scene.instance()
	spawn()
	player.global_position = Vector2(0, 0)

func spawn():
	player = player_scene.instance()
	GlobalWorldInfo.get_world().add_child(player)
	
