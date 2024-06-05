extends Area2D


func _process(delta):
	z_index = global_position.y / 2
	if overlaps_body(Player.get_body()):
		$price.visible = true
		$kinematic/cake.frame = 1
		if Input.is_action_just_pressed("E") and Player.get_hp() <= 35 and Player.get_money() > 5:
			EventBus.emit_signal("update_character_hp_bar_value", 5, Player.get_max_hp())
			Player.set_money(-5)
	else:
		$kinematic/cake.frame = 0
		$price.visible = false
