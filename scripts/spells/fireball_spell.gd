extends "res://scripts/spells/base_spell.gd"

func _init():
	rarity = "bronze"
	var spell_icon = load("res://sprites/spell_icons/fireball_icon.png")
	var background_texture = load("res://sprites/ui/%s_module_button_state.png" % rarity)
	inventory_item = inventory_item_scene.new(self, "spell", spell_icon, background_texture)
	animation_name = "fireball_cast"
	cooldown_time = 1
	time_spend = 1
	mana_cost = 3
	particle = load("res://scenes/fireball.tscn")



