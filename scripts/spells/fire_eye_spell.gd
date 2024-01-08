extends "res://scripts/spells/base_spell.gd"


func _init():
	rarity = "bronze"
	animation_name = "pillar_cast"
	cooldown_time = 5
	mana_cost = 10
	spell_icon = load("res://sprites/spell_icons/fire_eye_spell_icon.png")
	particle = load("res://scenes/fire_eye.tscn")
	

