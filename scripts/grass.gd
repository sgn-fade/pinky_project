extends StaticBody2D


func _ready():
	$sprite.frame = randi() % 6
	

func _on_visible_screen_entered():
	z_index = global_position.y
	visible = true


func _on_visible_screen_exited():
	visible = false


func _on_Area2D_body_entered(body):
	if body.name != "grass":
		if Player.get_position().x - global_position.x > 0:
			$anim.play("left")
		else:
			$anim.play("right")
			