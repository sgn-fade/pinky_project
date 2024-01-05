extends Node
var spell_icon = load("res://sprites/spell_icons/fire_pillar_icon.png")
var rarity = "bronze"
var fire_pillar
var is_ready = true
var cooldown_time = 8
var animation_name = "pillar_cast"

func _init():
	fire_pillar = load("res://scenes/particles/fire_pillar.tscn")


func cast():
	cooldown()
	GlobalWorldInfo.get_world_3d().add_child(fire_pillar.instantiate())
	EventBus.emit_signal("hands_play_animation", 1.5, animation_name)
	EventBus.emit_signal("player_cast_spell", 1.5, animation_name)
	#fire_pillar.queue_free()

func cooldown():
	is_ready = false
	await GlobalWorldInfo.get_world_3d().get_tree().create_timer(cooldown_time).timeout
	is_ready = true


func get_ready():
	return ready
