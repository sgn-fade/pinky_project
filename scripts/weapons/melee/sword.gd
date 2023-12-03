extends "res://scripts/weapons/melee/melee_weapon.gd"
onready var weapon_sprite_path = load("res://sprites/weapons/melee/goblin_sword.png")
var critchance = 24

func _init():
	icon = load("res://sprites/weapons/melee/goblin_sword_inventory.png")
	damage = 5
	combo_count = 2

func get_weapon_sprite_path():
	return weapon_sprite_path

func get_critchance():
	return critchance
