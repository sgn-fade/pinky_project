extends "res://scripts/weapons/weapon.gd"
var combo_count

func _init():
	type = "melee"

func get_combo_count():
	return combo_count
