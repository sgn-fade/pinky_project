extends Elementals
var current_state = null
var attack_cooldown = 0

enum States{
	IDLE,
	MOVE,
	ATTACK,
}
func _ready():
	hp = 40

func _process(delta):
	
	move(Player.global_position - global_position)
	
func move(direction):
	if hp > 0 && Player.get_hp() > 0 :
		swap_sprite_direction(direction)
		current_state = States.MOVE
		move_and_slide((direction).normalized() * speed)

func swap_sprite_direction(direction):
	if direction.x <= 0:
		$body.transform.x.x = 1

	elif direction.x > 0:
		$body.transform.x.x = -1

