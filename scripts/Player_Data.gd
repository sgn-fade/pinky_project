extends Node
var hp = 50
var mana = 30
var max_hp = 50
var max_mana = 30
var coins = 0
@onready var ui = get_node("/root/World/Ui")
@onready var player_scene = load("res://scenes/main_character.tscn")
@onready var player = null
@onready var book = load("res://scripts/weapons/magic_weapons/fire_book_tome_1.gd")

var score = 0

var weapon_slot_1 = null
var weapon_slot_2 = null
var weapon_current_slot = 1

var can_smite = false

var closest_interactive_object = null
var magic_damage = 1
func _ready():
	EventBus.emit_signal("update_character_hp_bar_value", hp, max_hp)
	EventBus.emit_signal("update_character_mana_bar_value", mana, max_mana)
	set_weapon(book.new())

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
	EventBus.emit_signal("update_character_hp_bar_value", hp, max_hp)

#PLAYER MANA
func get_mana():
	return mana

#returns true if mana successfully changed
func change_mana(value):
	if get_mana() < abs(value):
		player.throw_not_enough_mana_massage()
		return false
	mana += value
	EventBus.emit_signal("update_character_mana_bar_value", mana, max_mana)
	return true
	

func get_max_mana():
	return max_mana
func set_max_mana(value):
	max_mana += value
	EventBus.emit_signal("update_character_mana_bar_value", mana, max_mana)
##
#MAGIC DAMAGE
func set_magic_damage(new_magic_damage):
	magic_damage = new_magic_damage
func get_magic_damage():
	return magic_damage
##

#PlAYER POSITION
func get_position():
	if player == null:
		return Vector2.ZERO
	return player.global_position
func set_position(position):
	player.global_position = position
##

func ready():
	if player != null:
		return true
	return false

func get_body():
	return player

func set_z_index(value):
	player.z_index = value
func get_z_index():
	return player.z_index

#WEAPON
func get_weapon():
	if weapon_slot_1 == null and weapon_slot_2 == null:
		return null
	match get_weapon_current_slot():
		1:return weapon_slot_1
		2:return weapon_slot_2

func set_weapon(weapon):
	match get_weapon_current_slot():
		1:weapon_slot_1 = weapon
		2:weapon_slot_2 = weapon

func set_weapon_current_slot(value):
	weapon_current_slot = value
func get_weapon_current_slot():
	return weapon_current_slot



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
	player = player_scene.instantiate()
	spawn()
	player.global_position = Vector2(0, 0)

func spawn():
	player = player_scene.instantiate()
	GlobalWorldInfo.get_world().add_child(player)

#passive spells

func set_smite(state):
	can_smite = state

func get_smite(enemy):
	if can_smite:
		if enemy.hp <= 0 and enemy.global_position.distance_to(get_position()):
			return true
	return false

