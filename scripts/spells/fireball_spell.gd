extends Node

var animation_name = "cast"
var fireball
var spell_icon = load("res://sprites/spell_icons/fireball_icon.png")
var rarity = "bronze"
var is_ready = true
var cooldown_time = 1
func _init():
	fireball = load("res://scenes/fireball.tscn")


func cast():
	GlobalWorldInfo.get_world_3d().add_child(fireball.instantiate())
	EventBus.emit_signal("hands_play_animation", 0.2, animation_name)
	cooldown()

func cooldown():
	is_ready = false
	await GlobalWorldInfo.get_world_3d().get_tree().create_timer(cooldown_time).timeout
	is_ready = true

func get_ready():
	return is_ready
