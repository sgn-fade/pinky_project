extends Control

signal character_dead


func _update_character_hp_bar_value(hp, max_hp):
	$hp_bar.material.set_shader_parameter("fV", float(hp) / max_hp)
	$current_hp.text = str(hp)
	$max_hp.text = str(max_hp)

func _update_character_mana_bar_value(mana, max_mana):
	$mana_bar.material.set_shader_parameter("fV", float(mana) / max_mana)
	$current_mana.text = str(mana)
	$max_mana.text = str(max_mana)
