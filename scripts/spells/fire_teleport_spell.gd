extends Node
var spell_icon = load("res://sprites/spell_icons/fire_teleport_icon.png")
var rarity = "bronze"
var is_ready = true
var cooldown_time = 8 #8

func cast():
	cooldown()
	EventBus.emit_signal("player_teleport", Player.get_body().get_local_mouse_position())
	EventBus.emit_signal("hands_play_animation", 1.25, "teleport_start")
	await EventBus.spell_animation_ended
	
	EventBus.emit_signal("hands_play_animation", 0.833, "teleport_end")
	
	

func cooldown():
	is_ready = false
	await GlobalWorldInfo.get_world_3d().get_tree().create_timer(cooldown_time).timeout
	is_ready = true


func get_ready():
	return is_ready
