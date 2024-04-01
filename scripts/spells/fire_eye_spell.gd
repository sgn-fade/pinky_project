extends "res://scripts/spells/base_spell.gd"


func _init():
	rarity = "bronze"
	var spell_icon = load("res://sprites/spell_icons/fire_eye_spell_icon.png")
	var background_texture = load("res://sprites/ui/%s_module_button_state.png" % rarity)
	inventory_item = inventory_item_scene.new(self, "spell", spell_icon, background_texture)
	animation_name = "pillar_cast"
	cooldown_time = 5
	time_spend = 5
	mana_cost = 10
	particle = load("res://scenes/fire_eye.tscn")
	

