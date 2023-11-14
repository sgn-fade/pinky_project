extends Node
var spell_icon = load("res://sprites/spell_icons/fire_pillar_icon.png")
var rarity = "bronze"
var fire_pillar
var ready = true
var cooldown = 6
var animation_name = "pillar_cast"

func _init():
	fire_pillar = load("res://scenes/particles/fire_pillar.tscn").instance()


func cast():
	cooldown()
	GlobalWorld.add_child(fire_pillar)
	EventBus.emit_signal("hands_play_animation", 1.5, animation_name)
	EventBus.emit_signal("player_cast_spell", 1.5, animation_name)

func cooldown():
	ready = false
	yield(GlobalWorld.get_tree().create_timer(cooldown), "timeout")
	ready = true


func get_ready():
	return ready
