extends StaticBody2D



func _input(event):
	if Input.is_action_just_pressed("E"):
		if $Area2D.overlaps_body(Player.get_body()):
			EventBus.emit_signal("generate_dungeon")

func _on_Area2D_body_exited(body):
	if body == Player.get_body():
		$Button_picture.visible = false


func _on_Area2D_body_entered(body):
	$Button_picture.visible = true
