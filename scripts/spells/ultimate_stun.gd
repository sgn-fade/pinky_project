extends "res://scripts/spells/base_spell.gd"



func _init():
	rarity = "silver"
	animation_name = "fireball_cast"
	cooldown_time = 1
	time_spend = 1
	mana_cost = 3
	particle = load("res://scenes/ultimate_stun.tscn")
	spell_icon = load("res://sprites/spell_icons/fireball_icon.png")



