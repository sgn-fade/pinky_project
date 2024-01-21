extends "res://scripts/spells/base_spell.gd"

func _init():
	rarity = "bronze"
	animation_name = "spear_throw"
	cooldown_time = 4
	time_spend = 4
	mana_cost = 7
	particle = load("res://scenes/spell_particles/fire_spear_particle.tscn")
	spell_icon = load("res://sprites/spell_icons/fire_spear_spell_icon.png")



func cast():
	cooldown()
	EventBus.emit_signal("hands_play_animation", 0.58, animation_name)
	EventBus.emit_signal("player_cast_spell", 0.58, animation_name)
	#await GlobalWorldInfo.get_world().get_tree().create_timer(0.33).timeout
	#GlobalWorldInfo.get_world().add_child(particle.instantiate())


