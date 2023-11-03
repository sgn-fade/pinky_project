extends KinematicBody2D
var speed = 300
var mouse_pos = position
var character_pos = position
var velocity = Vector2()
var max_distance = 10000

func _process(delta):
	velocity = (mouse_pos - character_pos).normalized() * speed 
	if (global_position - character_pos).length() < max_distance: 
		velocity = move_and_slide(velocity) 
	else:
		delete()
	
func _on_body_entered(body):
	EventBus.emit_signal("damage_to_enemy", body, 1, null)
	delete()
	

func delete():
	set_process(false)
	queue_free()
