extends Node
var spell_icon = load("res://sprites/spell_icons/fire_pillar_icon.png")
var rarity = "bronze"
var fire_pillar
var ready = true
var cooldown = 1
var animation_name = "pillar_cast"

func _init():
	fire_pillar = load("res://scenes/particles/fire_pillar.tscn")


func cast():
	cooldown()
	GlobalWorldInfo.get_world().add_child(fire_pillar.instance())
	EventBus.emit_signal("hands_play_animation", 1.5, animation_name)
	EventBus.emit_signal("player_cast_spell", 1.5, animation_name)
	#fire_pillar.queue_free()

func cooldown():
	ready = false
	yield(GlobalWorldInfo.get_world().get_tree().create_timer(cooldown), "timeout")
	ready = true


func get_ready():
	return ready
