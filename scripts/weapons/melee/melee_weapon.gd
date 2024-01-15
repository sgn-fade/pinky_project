extends "res://scripts/weapons/weapon.gd"
@onready var base_hit
var combo_count

func _init():
	super._init()
	type = "melee"
	base_hit = load("res://scripts/spells/melee_spells/base_hit.gd")
	super.add_base_spell(base_hit.new())
func get_combo_count():
	return combo_count
