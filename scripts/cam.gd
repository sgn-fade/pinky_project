extends KinematicBody2D

func _process(delta):
	update_camera()
	move_and_slide((Player.get_position() - global_position) * 4)

func update_camera():
	if Input.is_action_just_released("wheel_up"):
		$cam.zoom.x -= 0.05
		$cam.zoom.y -= 0.05
	if Input.is_action_just_released("wheel_down"):
		$cam.zoom.x += 0.05
		$cam.zoom.y += 0.05

