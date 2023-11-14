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
	if $detection_area.overlaps_body(Player.get_body()):
		$collision.disabled = true
		current_state = States.MOVE


func move_state():
	direction = Player.get_position() - self.global_position
	move_and_slide(direction.normalized() * 100)
	if (Player.get_position() - self.global_position).length() < 7:
		Player.set_money(Player.get_money() + 1)
		self.queue_free()
