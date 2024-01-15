extends Node
var animation_name
var rarity
var is_ready = true
var cooldown_time
var mana_cost
var spell_icon
var particle

func cast():
	Player.change_mana(-mana_cost)
	if particle != null:
		GlobalWorldInfo.get_world().add_child(particle.instantiate())
	Player.set_state(Player.get_body().States.SPELL)
	EventBus.emit_signal("hands_play_animation", 0.2, animation_name)
	cooldown()

func cooldown():
	is_ready = false
	await GlobalWorldInfo.get_world().get_tree().create_timer(cooldown_time).timeout
	is_ready = true

func get_ready():
	return is_ready
