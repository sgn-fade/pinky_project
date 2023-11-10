extends Node
var spell_icon = load("res://sprites/spell_icons/fire_teleport_icon.png")
var rarity = "bronze"
var ready = true
var cooldown = 1 #8

func cast():
	cooldown()
	EventBus.emit_signal("player_teleport", Player.get_body().get_local_mouse_position())
	EventBus.emit_signal("hands_play_animation", 1.25, "teleport_start")
	yield(EventBus, "spell_animation_ended")
	
	EventBus.emit_signal("hands_play_animation", 0.833, "teleport_end")
	
	

func cooldown():
	ready = false
	yield(GlobalWorldInfo.get_world().get_tree().create_timer(cooldown), "timeout")
	ready = true


func get_ready():
	return ready
