extends "res://scripts/spells/base_spell.gd"


func cast():
	cooldown()
	EventBus.emit_signal("player_teleport", Player.get_body().get_local_mouse_position())
	EventBus.emit_signal("hands_play_animation", 1.25, "teleport_start")
	await EventBus.spell_animation_ended
	EventBus.emit_signal("hands_play_animation", 0.833, "teleport_end")


func _init():
	rarity = "bronze"
	var spell_icon = load("res://sprites/spell_icons/fire_teleport_icon.png")
	var background_texture = load("res://sprites/ui/%s_module_button_state.png" % rarity)
	inventory_item = inventory_item_scene.new(self, "spell", spell_icon, background_texture)
	animation_name = null
	cooldown_time = 8
	time_spend = 8
	mana_cost = 20
	particle = null
