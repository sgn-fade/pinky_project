extends Node
var spell_icon = load("res://sprites/spell_icons/fire_eye_spell_icon.png")
var rarity = "bronze"
var fire_eye
var is_ready = true
var cooldown_time = 3
var animation_name = "pillar_cast"

func _init():
	fire_eye = load("res://scenes/fire_eye.tscn")
	
func cast():
	cooldown()
	GlobalWorldInfo.get_world_3d().add_child(fire_eye.instantiate())
	EventBus.emit_signal("hands_play_animation", 1.5, animation_name)
	
func cooldown():
	is_ready = false
	await GlobalWorldInfo.get_world_3d().get_tree().create_timer(cooldown_time).timeout
	is_ready = true


func get_ready():
	return is_ready
