extends StaticBody2D

func _process(delta):
	if $Area2D.overlaps_body(Player):
		$Button_picture.visible = true
		if Input.is_action_just_pressed("pick_up"):
			set_process(false)
			$box.frame = 0
			$box.play("opening")
	else:
		$Button_picture.visible = false
		
