#script for a group of goblins 
extends Enemies
class_name Goblins
var sprite = null
var pull_source = Vector2.ZERO
enum States {
	IDLE,
	MOVE,
	PULLS,
	ATTACK,
	DEALS_DAMAGE,
	SEARCHING,
	NONE
}
var current_state = States.IDLE
var gold_drop = preload("res://scenes/drops/gold_drop.tscn")

var type = "goblin"

func randomize_direction():
	match randi()%4 + 1:
		1:return Vector2(-1, 0)
		2:return Vector2(1, 0)
		3:return Vector2(0, -1)
		4:return Vector2(0, 1)

func _ready():
	super._ready()

func play_animation(name):
	sprite.play(name)
	await sprite.animation_finished

func swap_sprite_direction():
	if direction.x <= 0:
		transform.x.x = -1
	elif direction.x > 0:
		transform.x.x = 1

func searching_player(delta):
	if global_position.distance_to(Player.get_position()) < 100:
		current_state = States.MOVE
		return

func move(dir):
	direction = dir
	set_velocity((direction).normalized() * speed)
	move_and_slide()
	#if (direction.length() >= 1000):
		#current_state = States.IDLE


func spawn_drop():
	randomize()
	for i in randi() % 6:
		var coins = gold_drop.instantiate()
		GlobalWorldInfo.get_world().add_child(coins)
		coins.global_position = self.global_position + Vector2(randi()%11 - 5, randi()%11 - 5)


func _on_damage_to_enemy(body, damage, status):
	super._on_damage_to_enemy(body, damage, status)

func attract(delta):
	velocity = velocity.lerp((pull_source - global_position).normalized() * 50, delta / 0.1)
	set_velocity(velocity)
	move_and_slide()
	if (pull_source - global_position).length() <= 3:
		slowdown()
		current_state = States.MOVE
func _on_pulls_body(body, pos):
	if body == self:
		pull_source = pos
		current_state = States.PULLS
	
