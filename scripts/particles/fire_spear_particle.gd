extends CharacterBody2D
var speed = 300


func _ready():
	$area.connect("body_entered", Callable(self, "_on_body_entered"))
	var end_position = get_global_mouse_position()
	global_position = Player.get_position()
	velocity = end_position - global_position
	look_at(get_global_mouse_position()) 


func _process(delta):
	velocity = velocity.normalized() * speed
	set_velocity(velocity)
	move_and_slide()
	velocity = velocity 
	
	
func delete():
	queue_free()
	


func _on_body_entered(body):
	EventBus.emit_signal("damage_to_enemy", body, 10, null)
	EventBus.emit_signal("push_away_enemy", body, velocity)
	delete()

