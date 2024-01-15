extends CharacterBody2D
var speed = 300
var mouse_pos = position
var character_pos = position

var max_distance = 10000

func _process(delta):
	velocity = (mouse_pos - character_pos).normalized() * speed 
	if (global_position - character_pos).length() < max_distance: 
		set_velocity(velocity)
		move_and_slide()
		velocity = velocity 
	else:
		delete()
	
func _on_body_entered(body):
	if body.name == "Enemies":
		EventBus.emit_signal("damage_to_enemy", body, 1, null)
	delete()
	

func delete():
	set_process(false)
	queue_free()
