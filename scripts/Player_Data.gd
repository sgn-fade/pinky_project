extends Node
var hp = 50
var max_hp = 50
onready var ui = get_node("/root/World/Ui")
onready var player = get_node("/root/World/player")


func _ready():
	EventBus.emit_signal("update_character_hp_bar_value", hp, max_hp)


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


func get_body():
	return player

func get_z_index():
	return player.z_index


func get_weapon():
	return player.weapon

func set_weapon(weapon):
	player.weapon = weapon
