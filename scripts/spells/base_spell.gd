extends Node
var animation_name
var rarity
var is_ready = true
var cooldown_time
var time_spend = 0
var mana_cost
var spell_icon
var particle


func cast():
	Player.change_mana(-mana_cost)
	if particle != null:
		GlobalWorldInfo.get_world().add_child(particle.instantiate())
	EventBus.emit_signal("hands_play_animation", 0.2, animation_name)
	cooldown()


func get_ready():
	return true if not time_spend < cooldown_time else false

func cooldown():
	time_spend = 0
	is_ready = false

func get_cooldown_time():
	return time_spend
func get_max_cooldown_time():
	return cooldown_time
func set_cooldown_time(value):
	time_spend += value
