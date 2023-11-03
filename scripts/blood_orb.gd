extends KinematicBody2D
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
	move_and_slide(Vector2(0, offcet_sign))
	y_offcet += offcet_sign
	if $area.overlaps_body(Player):
		current_state = States.MOVE


func move_state():
	direction = Player.global_position - self.global_position
	move_and_slide(direction.normalized() * 100)
	if (Player.global_position - self.global_position).length() < 7:
		self.queue_free()
