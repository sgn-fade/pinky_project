
extends KinematicBody2D
var acceleration = 20
var dash_cooldown = 0

#resources
var coins = 0
#stats
var speed = 20
var magic_damage = 1
var hp = 40

var closest_interactive_object = null

var velocity = Vector2.ZERO
onready var _animated_sprite = get_node("/root/World/player/aSprite")
onready var dash = get_node("/root/World/Ui/game_ui/dash_indicator")
onready var ui = get_node("/root/World/Ui")
var weapon = null
var input = Vector2.ZERO

var timer := Timer.new()

var dash_speed_const = 1
var move_position = null
var direction = 1
var current_state = States.IDLE

enum States{
	NONE,
	IDLE,
	MOVE,
	DASH,
	BUTT_HIT_DASH,
	SPELL,
	DEAD,
}
func _process(delta):
	z_index = global_position.y / 2

	match current_state:
		States.NONE:
			pass
		States.IDLE:
			play_animation("idle")
			move()
		States.MOVE:
			play_animation("move")
			move()
			dash(delta)
		States.DASH:
			move_player(delta)
		States.BUTT_HIT_DASH:
			pass
		States.SPELL:
			pass
		States.DEAD:
			pass

func rotating():
	if get_local_mouse_position().x >= 0:
		scale.x = 1
	else:
		direction *= -1
		scale.x = -1


func _ready():
	set_hide_state(true)
	EventBus.emit_signal("hands_play_animation",0, "idle")
	#$teleport_ray.add_exception($player_area)
	EventBus.connect("player_take_damage", self, "_on_player_take_damage")
	EventBus.connect("player_teleport", self, "teleport")
	EventBus.connect("load_game", self, "load_game")
	EventBus.connect("player_cast_spell", self, "set_cast_state")
	timer.one_shot = false
	add_child(timer)
	Input.set_mouse_mode(1)

func _input(event):
	if weapon != null:
		weapon.input(event)


func move():
	input = Vector2.ZERO
	var animation = "move"
	current_state = States.MOVE
	if Input.is_action_pressed("ui_right"):
		input.x += 1
		transform.x.x = 1
	if Input.is_action_pressed("ui_left"):
		input.x += -1
		transform.x.x = -1
	if Input.is_action_pressed("ui_up"):
		input.y += -1
	if Input.is_action_pressed("ui_down"):
		input.y += 1
	
	if input.length() == 0:
		if speed >30:
			speed = 30
		animation = "idle"
		current_state = States.IDLE
	elif speed < 80:
		speed += 5
	input = input.normalized()

	velocity = velocity.linear_interpolate(input * speed, acceleration * 0.016)
	move_and_slide(velocity)



func play_animation(animation):

	_animated_sprite.play(animation)
	EventBus.emit_signal("hands_play_animation",0, animation)

func move_player(delta):
	var t = 0.05
	velocity = velocity.linear_interpolate(move_position, 0.016 / t)
	if velocity.length() >= move_position.length() - 1:
		current_state = States.IDLE
	move_and_slide(velocity)
	dash_speed_const += 1
	

func dash(delta):
	if Input.is_action_just_pressed("Shift") && dash.ready && current_state != States.DASH:
		move_position = input * 200
		
		current_state = States.DASH
		$dash_particles1.emitting = true
		$dash_particles2.emitting = true
		disable_collision()
		EventBus.emit_signal("dash_cooldown")
		character_slowdown()
		


func change_state(state):
	current_state = state

func die():
	current_state = States.DEAD
	EventBus.emit_signal("player_dead")


func set_idle_state():
	visible = true
	current_state = States.IDLE

func set_hide_state(state):
	visible = !state
	if state:
		current_state = States.NONE

func load_game():
	set_hide_state(false)
func character_slowdown():
	speed = 0
	while speed < 20:
		speed += 5
		timer.start(0.05)
		yield(timer, "timeout")


func disable_collision():
	set_collision_mask_bit(2, false)
	$player_area.set_collision_mask_bit(2, false)


func enable_collision():
	$player_area.set_collision_mask_bit(2, true)
	set_collision_mask_bit(2, true)


func _on_player_take_damage(player_offcet_dir, enemy_damage):
	Player.update_hp(-enemy_damage)
	var hp = Player.get_hp() 
	var max_hp = Player.get_max_hp() 
	EventBus.emit_signal("update_character_hp_bar_value", hp, max_hp)
	disable_collision()
	velocity = velocity.linear_interpolate(player_offcet_dir * 1000, 0.40)
	move_and_slide(velocity)
	if Player.get_hp() <= 0:
		die()
	if hp >= 5:
		for i in 3:
			modulate = "49ffffff"
			timer.start(0.07)
			yield(timer, "timeout")
			modulate = "ffffff"
			timer.start(0.07)
			yield(timer, "timeout")
	enable_collision()



func c_shotgun_recoil():
	var player_offcet_dir = (-(get_global_mouse_position() - global_position).normalized())
	for i in 8:
			velocity = velocity.linear_interpolate(player_offcet_dir * 20, 0.40)
			move_and_slide(velocity)
			timer.start(0.005)
			yield(timer, "timeout")

func push_body():
	var player_offcet_dir = (get_global_mouse_position() - global_position).normalized()
	for i in 8:
			velocity = velocity.linear_interpolate(player_offcet_dir * 50, 0.40)
			move_and_slide(velocity)
			timer.start(0.005)
			yield(timer, "timeout")


func set_cast_state(animation_time, animation_name):
	current_state = States.SPELL
	_animated_sprite.play(animation_name)
	timer.start(animation_time)
	yield(timer, "timeout")
	current_state = States.IDLE


func teleport(pos):
	current_state = States.SPELL
	$teleport_ray.cast_to = pos
	var global_mouse_pos = get_global_mouse_position()
	_animated_sprite.play("teleport_start")
	yield(_animated_sprite, "animation_finished")
	
	disable_collision()
	if $teleport_ray.is_colliding():
		global_position = $teleport_ray.get_collision_point() 
		global_position += Vector2(-8 * direction, -4 -4 * pos.sign().y)
	else:
		global_position = global_mouse_pos
	enable_collision()
	_animated_sprite.play("teleport_end")
	yield(_animated_sprite, "animation_finished")
	current_state = States.IDLE
	
func set_inventory_state():
	_animated_sprite.play("idle")
	current_state = States.INVENTORY



