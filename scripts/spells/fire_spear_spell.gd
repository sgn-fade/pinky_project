extends Node
var spell_icon = load("res://sprites/spell_icons/fire_spear_spell_icon.png")
var rarity = "bronze"
var fire_spear
var is_ready = true
var cooldown_time = 4
var animation_name = "spear_throw"

func _init():
	fire_spear = load("res://scenes/spell_particles/fire_spear_spell.tscn")


func cast():
	cooldown()
	EventBus.emit_signal("hands_play_animation", 0.58, animation_name)
	EventBus.emit_signal("player_cast_spell", 0.58, animation_name)
	await GlobalWorldInfo.get_world_3d().get_tree().create_timer(0.33).timeout
	GlobalWorldInfo.get_world_3d().add_child(fire_spear.instantiate())

func cooldown():
	is_ready = false
	await GlobalWorldInfo.get_world_3d().get_tree().create_timer(cooldown_time).timeout
	is_ready = true


func get_ready():
	return is_ready
