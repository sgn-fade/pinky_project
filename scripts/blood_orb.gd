extends CharacterBody2D
var direction = Vector2.ZERO
enum States{
	IDLE,
	MOVE
}
var y_offcet = 0
var offcet_sign = 1
var current_state = States.IDLE


func _process(delta):
	
	match current_state:
		States.IDLE:
			idle_state()
		States.MOVE:
			move_state()

func idle_state():
	if y_offcet >= 40:
		offcet_sign = -1
	elif y_offcet <= 0:
		offcet_sign = 1
	set_velocity(Vector2(0, offcet_sign))
	move_and_slide()
	y_offcet += offcet_sign
	if $area.overlaps_body(Player):
		current_state = States.MOVE


func move_state():
	direction = Player.global_position - self.global_position
	set_velocity(direction.normalized() * 100)
	move_and_slide()
	if (Player.global_position - self.global_position).length() < 7:
		self.queue_free()
