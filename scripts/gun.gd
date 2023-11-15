extends StaticBody2D

onready var world = get_node("/root/World")
onready var player_sprite = get_node("/root/World/main_character/AnimatedSprite")
onready var sprite = $sprite
var shoot_cooldown = 0
var timer := Timer.new()
var preparation_animation_time = 0
enum Default_States{
	IDLE,
	SHOOT,
	RELOAD,
}
var current_state = Default_States.IDLE

func creating():
	add_child(timer)
	timer.one_shot = false
	


func rotating():
	look_at(get_global_mouse_position())


func set_idle_state():
	sprite.play("idle")
	sprite.stop()
	sprite.frame = 0
	current_state = Default_States.IDLE
