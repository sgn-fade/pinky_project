extends Node

var animation_name = "cast"
var fireball
var spell_icon = load("res://sprites/spell_icons/fireball_icon.png")
var rarity = "bronze"
var ready = true
var cooldown = 1
func _init():
	fireball = load("res://scenes/fireball.tscn")


func cast():
	GlobalWorldInfo.get_world().add_child(fireball.instance())
	EventBus.emit_signal("hands_play_animation", 0.2, animation_name)
	cooldown()

func cooldown():
	ready = false
	yield(GlobalWorldInfo.get_world().get_tree().create_timer(cooldown), "timeout")
	ready = true

func get_ready():
	return ready
