extends "res://scripts/spells/base_spell.gd"

func _init():
	rarity = "bronze"
	var spell_icon = load("res://sprites/spell_icons/sword_hit.png")
	var background_texture = load("res://sprites/ui/%s_module_button_state.png" % rarity)
	inventory_item = inventory_item_scene.new(self, "spell", spell_icon, background_texture)
	animation_name = "null"
	cooldown_time = 0.5
	time_spend = 0.5
	mana_cost = 0
	particle = null

