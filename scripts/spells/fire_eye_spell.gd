extends Node
var spell_icon = load("res://sprites/spell_icons/fire_eye_spell_icon.png")
var rarity = "bronze"
var fire_eye
var ready = true
var cooldown = 3
var animation_name = "pillar_cast"

func _init():
	fire_eye = load("res://scenes/fire_eye.tscn")
	
func cast():
	cooldown()
	GlobalWorld.add_child(fire_eye.instance())
	EventBus.emit_signal("hands_play_animation", 1.5, animation_name)
	
func cooldown():
	ready = false
	yield(GlobalWorld.get_tree().create_timer(cooldown), "timeout")
	ready = true


func get_ready():
	return ready
