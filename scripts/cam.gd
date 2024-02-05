extends CharacterBody2D
var focused_object = null
func _process(delta):
	if focused_object != null:
		set_velocity((Player.get_position() + (focused_object.global_position - Player.get_position()) / 4 - global_position) * 4)
	else:
		set_velocity((Player.get_position() - global_position) * 4)
	move_and_slide()
	update_camera()


func update_camera():
	if Input.is_action_just_released("wheel_up"):
		$cam.zoom.x -= 0.05
		$cam.zoom.y -= 0.05
	if Input.is_action_just_released("wheel_down"):
		$cam.zoom.x += 0.05
		$cam.zoom.y += 0.05


func set_view(object):
	if focused_object != null:
		focused_object.set_focused(false)
		focused_object = null
		return
	if object != null:
		object.set_focused(true)
		focused_object = object
