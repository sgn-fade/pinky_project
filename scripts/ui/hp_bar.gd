extends Control

signal character_dead
func _ready():
	EventBus.connect("update_character_hp_bar_value", Callable(self, "_update_character_hp_bar_value"))


func _update_character_hp_bar_value(hp, max_hp):
	$hp_bar.material.set_shader_parameter("fV", float(hp) / max_hp)
	$current_hp.text = String(hp)
	$max_hp.text = String(max_hp)
