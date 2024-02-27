extends "res://scripts/spells/base_spell.gd"

func _init():
	rarity = "bronze"
	animation_name = "null"
	cooldown_time = 0
	mana_cost = 0
	particle = null
	spell_icon = load("res://sprites/spell_icons/sword_smite.png")
	Player.set_smite(true)
	
