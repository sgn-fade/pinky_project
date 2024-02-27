extends Node2D


func _process(delta):
	if $Area2D.overlaps_body(Player.get_body()):
		if Input.is_action_just_pressed("E"):
			set_process(false)
			EventBus.emit_signal("go_to_hub")
			queue_free()

