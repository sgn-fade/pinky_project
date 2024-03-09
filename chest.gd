extends StaticBody2D


func _input(event):
	if Input.is_action_just_pressed("E"):
		if $Area2D.overlaps_body(Player.get_body()):
			$sprite.play("open")
