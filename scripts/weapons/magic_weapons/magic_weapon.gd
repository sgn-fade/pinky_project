extends "res://scripts/weapons/weapon.gd"

var spell_module_buffs = []
var mana #mb

func _init():
	super._init()
	type = "magic"
