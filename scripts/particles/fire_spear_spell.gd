extends KinematicBody2D
var velocity = Vector2.ZERO
var speed = 300


func _ready():
	$area.connect("body_entered", self, "_on_body_entered")
	var end_position = get_global_mouse_position()
	global_position = Player.get_position()
	velocity = end_position - global_position
	look_at(get_global_mouse_position()) 


func _process(delta):
	velocity = velocity.normalized() * speed
	velocity = move_and_slide(velocity) 
	
	
func delete():
	queue_free()
	


func _on_body_entered(body):
	EventBus.emit_signal("damage_to_enemy", body, 3, null)
	EventBus.emit_signal("push_away_enemy", body, velocity)
	delete()

