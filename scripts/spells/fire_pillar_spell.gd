extends "res://scripts/spells/base_spell.gd"


func _init():
	rarity = "bronze"
	animation_name = "pillar_cast"
	cooldown_time = 8
	mana_cost = 13
	particle = load("res://scenes/particles/fire_pillar.tscn")
	spell_icon = load("res://sprites/spell_icons/fire_pillar_icon.png")

