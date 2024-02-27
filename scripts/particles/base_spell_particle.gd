extends CharacterBody2D
var timer := Timer.new()
var speed = 300
func _ready():
	timer.one_shot = false
	add_child(timer)
	var end_position = get_global_mouse_position()
	self.global_position = Player.get_position()
	self.global_position.y -= 5
	velocity = (end_position - Player.get_position()).normalized() * speed
	look_at(end_position) 

func _process(delta):
	velocity = velocity.normalized() * speed
	set_velocity(velocity)
	move_and_slide()
	velocity = velocity 

func _on_body_entered(body):
	delete()
	EventBus.emit_signal("damage_to_enemy", body, 10, null)
	
func delete():
	queue_free()
