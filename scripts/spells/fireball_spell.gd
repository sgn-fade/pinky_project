extends "res://scripts/spells/base_spell.gd"



func _init():
	rarity = "bronze"
	animation_name = "fireball_cast"
	cooldown_time = 1
	mana_cost = 3
	particle = load("res://scenes/fireball.tscn")
	spell_icon = load("res://sprites/spell_icons/fireball_icon.png")



