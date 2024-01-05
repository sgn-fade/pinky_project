extends CharacterBody2D

func _process(delta):
	update_camera()
	set_velocity((Player.get_position() - global_position) * 4)
	move_and_slide()

func update_camera():
	if Input.is_action_just_released("wheel_up"):
		$cam.zoom.x -= 0.05
		$cam.zoom.y -= 0.05
	if Input.is_action_just_released("wheel_down"):
		$cam.zoom.x += 0.05
		$cam.zoom.y += 0.05

